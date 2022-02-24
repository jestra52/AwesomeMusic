namespace AwesomeMusic.Data.Commands.UserCommands
{
    using AwesomeMusic.Data.DTOs;

    public class AuthenticateCommand : CommandBase<Response<AuthResponseDto>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
