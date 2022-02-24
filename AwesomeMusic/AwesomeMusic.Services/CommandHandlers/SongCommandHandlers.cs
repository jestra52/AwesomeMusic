namespace AwesomeMusic.Services.CommandHandlers
{
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AwesomeMusic.Data.Commands.SongCommands;
    using AwesomeMusic.Data.DTOs;
    using AwesomeMusic.Data.Model;
    using AwesomeMusic.Data.Model.Entities;
    using AwesomeMusic.Services.Utility;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    public class SongCommandHandlers
    {
        public class CreateSongCommandHandler : IRequestHandler<CreateSongCommand, Response<SongDto>>
        {
            private readonly IAwesomeMusicContext _context;
            private readonly IIdentityUtility _identityUtility;
            private readonly IMapper _mapper;

            public CreateSongCommandHandler(
                IAwesomeMusicContext context,
                IIdentityUtility identityUtility,
                IMapper mapper)
            {
                _context = context;
                _identityUtility = identityUtility;
                _mapper = mapper;
            }

            public async Task<Response<SongDto>> Handle(CreateSongCommand request, CancellationToken cancellationToken)
            {
                var response = new Response<SongDto>();
                var song = _mapper.Map<Song>(request);

                song.RegisteredById = int.Parse(_identityUtility.GetNameIdentifier());

                await _context.Songs.AddAsync(song, cancellationToken);
                await _context.SaveAsync(cancellationToken);
                response.Result = _mapper.Map<SongDto>(song);
                response.Message = "Song created successfully";

                return response;
            }
        }

        public class UpdateSongCommandHandler : IRequestHandler<UpdateSongCommand, Response<SongDto>>
        {
            private readonly IAwesomeMusicContext _context;
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly IIdentityUtility _identityUtility;
            private readonly IMapper _mapper;

            public UpdateSongCommandHandler(
                IAwesomeMusicContext context,
                IHttpContextAccessor httpContextAccessor,
                IIdentityUtility identityUtility,
                IMapper mapper)
            {
                _context = context;
                _httpContextAccessor = httpContextAccessor;
                _identityUtility = identityUtility;
                _mapper = mapper;
            }

            public async Task<Response<SongDto>> Handle(UpdateSongCommand request, CancellationToken cancellationToken)
            {
                var response = new Response<SongDto>();
                var song = await _context.Songs.FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
                var userId = int.Parse(_identityUtility.GetNameIdentifier());

                if (song == null)
                {
                    _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Error = true;
                    response.Message = "Song not found";
                    return response;
                }

                if (song.RegisteredById != userId)
                {
                    _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Error = true;
                    response.Message = "You can only update songs of your own";
                    return response;
                }

                _mapper.Map(request, song);
                await _context.SaveAsync(cancellationToken);
                response.Result = _mapper.Map<SongDto>(song);
                response.Message = "Song updated successfully";

                return response;
            }
        }

        public class DeleteSongCommandHandler : IRequestHandler<DeleteSongCommand, Response<bool>>
        {
            private readonly IAwesomeMusicContext _context;
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly IIdentityUtility _identityUtility;
            private readonly IMapper _mapper;

            public DeleteSongCommandHandler(
                IAwesomeMusicContext context,
                IHttpContextAccessor httpContextAccessor,
                IIdentityUtility identityUtility,
                IMapper mapper)
            {
                _context = context;
                _httpContextAccessor = httpContextAccessor;
                _identityUtility = identityUtility;
                _mapper = mapper;
            }

            public async Task<Response<bool>> Handle(DeleteSongCommand request, CancellationToken cancellationToken)
            {
                var response = new Response<bool>();
                var song = await _context.Songs.FirstOrDefaultAsync(s => s.Id == request.SongId, cancellationToken);
                var userId = int.Parse(_identityUtility.GetNameIdentifier());

                if (song == null)
                {
                    _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Error = true;
                    response.Message = "Song not found";
                    return response;
                }

                if (song.RegisteredById != userId)
                {
                    _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Error = true;
                    response.Message = "You can only delete songs of your own";
                    return response;
                }

                _context.Songs.Remove(song);
                await _context.SaveAsync(cancellationToken);
                response.Result = true;
                response.Message = "Song deleted successfully";

                return response;
            }
        }
    }
}
