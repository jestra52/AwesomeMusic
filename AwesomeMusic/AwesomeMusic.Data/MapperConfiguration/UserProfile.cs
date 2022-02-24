namespace AwesomeMusic.Data.MapperConfiguration
{
    using AutoMapper;
    using AwesomeMusic.Data.Commands.UserCommands;
    using AwesomeMusic.Data.DTOs;
    using AwesomeMusic.Data.Model.Entities;

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();

            CreateMap<CreateUserCommand, User>()
                .ForMember(t => t.Email, opts => opts.MapFrom(s => s.Email.Trim()));
        }
    }
}
