namespace AwesomeMusic.Data.Queries.SongQueries
{
    using System.Collections.Generic;
    using AwesomeMusic.Data.DTOs;
    using MediatR;

    public class GetAllSongsQuery : IRequest<Response<IReadOnlyCollection<SongDto>>>
    {
        public bool? IncludePublic { get; set; } = true;
    }
}
