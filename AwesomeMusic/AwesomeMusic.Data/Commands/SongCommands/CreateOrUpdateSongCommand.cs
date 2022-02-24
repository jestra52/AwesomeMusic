namespace AwesomeMusic.Data.Commands.SongCommands
{
    using AwesomeMusic.Data.DTOs;

    public class CreateOrUpdateSongCommand : CommandBase<Response<SongDto>>
    {
        public string Name { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public int? Year { get; set; }
        public string Url { get; set; }
        public string CoverUrl { get; set; }
        public bool IsPublic { get; set; }
    }

    public class CreateSongCommand : CreateOrUpdateSongCommand
    { }

    public class UpdateSongCommand : CreateOrUpdateSongCommand
    {
        public int Id { get; set; }
    }
}
