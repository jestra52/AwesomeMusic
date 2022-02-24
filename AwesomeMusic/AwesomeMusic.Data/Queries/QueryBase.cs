namespace AwesomeMusic.Data.Queries
{
    using MediatR;

    public abstract class QueryBase<TResult> : IRequest<TResult> where TResult : class
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
