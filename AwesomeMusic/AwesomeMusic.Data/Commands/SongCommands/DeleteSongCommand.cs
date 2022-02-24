namespace AwesomeMusic.Data.Commands.SongCommands
{
    using AwesomeMusic.Data.DTOs;

    public class DeleteSongCommand : CommandBase<Response<bool>>
    {
        public int SongId { get; set; }
    }
}
