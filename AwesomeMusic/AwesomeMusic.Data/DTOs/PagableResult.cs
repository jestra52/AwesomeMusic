namespace AwesomeMusic.Data.DTOs
{
    using System.Collections.Generic;

    public class PageableResult<T> where T : class
    {
        public IEnumerable<T> Results { get; set; }
        public int TotalRecordCount { get; set; }
    }
}
