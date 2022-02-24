namespace AwesomeMusic.Data.Commands.UserCommands
{
    using AwesomeMusic.Data.DTOs;

    public class CreateUserCommand : CommandBase<Response<UserDto>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
}
