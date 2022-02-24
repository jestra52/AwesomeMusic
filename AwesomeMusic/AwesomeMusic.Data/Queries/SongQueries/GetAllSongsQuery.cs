namespace AwesomeMusic.Data.Queries.SongQueries
{
    using AwesomeMusic.Data.DTOs;
    using MediatR;

    public class GetAllSongsQuery : QueryBase<Response<PageableResult<SongDto>>>
    {
        public bool? IncludePublic { get; set; } = true;
    }
}
