namespace AwesomeMusic.Services.QueryHandlers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using AwesomeMusic.Data.DTOs;
    using AwesomeMusic.Data.Model;
    using AwesomeMusic.Data.Queries.SongQueries;
    using AwesomeMusic.Services.Extensions;
    using AwesomeMusic.Services.Utility;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class SongQueryHandlers
    {
        public class GetAllSongsQueryHandler : IRequestHandler<GetAllSongsQuery, Response<PageableResult<SongDto>>>
        {
            private readonly IAwesomeMusicContext _context;
            private readonly IIdentityUtility _identityUtility;
            private readonly IMapper _mapper;

            public GetAllSongsQueryHandler(
                IAwesomeMusicContext context,
                IIdentityUtility identityUtility,
                IMapper mapper)
            {
                _context = context;
                _identityUtility = identityUtility;
                _mapper = mapper;
            }

            public async Task<Response<PageableResult<SongDto>>> Handle(GetAllSongsQuery request, CancellationToken cancellationToken)
            {
                var response = new Response<PageableResult<SongDto>>();
                var userId = int.TryParse(_identityUtility.GetNameIdentifier(), out int result) ? result : 0;

                if (userId == 0)
                {
                    response.Error = true;
                    response.Message = "There was an error getting user id from session";
                    return response;
                }

                var query = _context.Songs
                    .Include(e => e.User)
                    .Where(s => s.RegisteredById == userId || s.IsPublic)
                    .AsQueryable();

                if (!request.IncludePublic.GetValueOrDefault())
                {
                    query = query.Where(s => !s.IsPublic);
                }

                query = query.GetPaged(request.PageSize, request.PageNumber);

                response.Result = new PageableResult<SongDto>
                {
                    TotalRecordCount = await query.CountAsync(cancellationToken),
                    Results = await query
                        .ProjectTo<SongDto>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken)
                };

                return response;
            }
        }
    }
}
