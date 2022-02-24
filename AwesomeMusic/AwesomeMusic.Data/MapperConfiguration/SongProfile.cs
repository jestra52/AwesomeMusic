namespace AwesomeMusic.Data.MapperConfiguration
{
    using AutoMapper;
    using AwesomeMusic.Data.Commands.SongCommands;
    using AwesomeMusic.Data.DTOs;
    using AwesomeMusic.Data.Model.Entities;

    public class SongProfile : Profile
    {
        public SongProfile()
        {
            CreateMap<Song, SongDto>()
                .ForMember(t => t.RegisteredBy, opts => opts.MapFrom(s => s.User.Name));

            CreateMap<CreateSongCommand, Song>();
            CreateMap<UpdateSongCommand, Song>(MemberList.Source)
                .ForMember(t => t.Id, opts => opts.Ignore());
        }
    }
}
