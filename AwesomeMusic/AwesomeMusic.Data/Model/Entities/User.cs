namespace AwesomeMusic.Data.Model.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text.Json.Serialization;

    [Table("User", Schema = "awm")]
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Song> Songs { get; set; }
    }
}
