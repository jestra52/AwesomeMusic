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
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class SongQueryHandlers
    {
        public class GetAllSongsQueryHandler : IRequestHandler<GetAllSongsQuery, Response<IReadOnlyCollection<SongDto>>>
        {
            private readonly IAwesomeMusicContext _context;
            private readonly IMapper _mapper;

            public GetAllSongsQueryHandler(IAwesomeMusicContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Response<IReadOnlyCollection<SongDto>>> Handle(GetAllSongsQuery request, CancellationToken cancellationToken)
            {
                var query = _context.Songs
                    .Include(e => e.User)
                    .AsQueryable();

                if (!request.IncludePublic.GetValueOrDefault())
                {
                    query = query.Where(s => !s.IsPublic);
                }

                var response = new Response<IReadOnlyCollection<SongDto>>
                {
                    Result = await query
                    .ProjectTo<SongDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken)
                };

                return response;
            }
        }
    }
}
