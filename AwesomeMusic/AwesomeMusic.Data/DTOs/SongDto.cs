namespace AwesomeMusic.Data.DTOs
{
    public class SongDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public int? Year { get; set; }
        public string Url { get; set; }
        public string CoverUrl { get; set; }
        public bool IsPublic { get; set; }

        public string RegisteredBy { get; set; }
    }
}
