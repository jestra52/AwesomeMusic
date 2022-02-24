namespace AwesomeMusic.Data.Model.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text.Json.Serialization;

    [Table("Song", Schema = "awm")]
    public class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public int? Year { get; set; }
        public string Url { get; set; }
        public string CoverUrl { get; set; }
        public bool IsPublic { get; set; }


        public int RegisteredById { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
