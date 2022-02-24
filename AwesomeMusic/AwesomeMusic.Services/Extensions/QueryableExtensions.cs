namespace AwesomeMusic.Services.Extensions
{
    using System.Linq;

    public static class QueryableExtensions
    {
        public static IQueryable<T> GetPaged<T>(this IQueryable<T> query, int? pageSize, int? pageNumber) where T : class
        {
            if (pageSize > 0 && pageNumber > 0)
            {
                var excludedRows = (pageNumber - 1) * pageSize;
                query = query.Skip(excludedRows.Value)
                    .Take(pageSize.Value);
            }

            return query;
        }
    }
}
