namespace AwesomeMusic.Test
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AwesomeMusic.Data.MapperConfiguration;
    using AwesomeMusic.Data.Model;
    using AwesomeMusic.Data.Model.Entities;
    using AwesomeMusic.Data.Queries.SongQueries;
    using AwesomeMusic.Services.QueryHandlers;
    using AwesomeMusic.Services.Utility;
    using AwesomeMusic.Test.Utils;
    using FluentAssertions;
    using MockQueryable.NSubstitute;
    using NSubstitute;
    using Xunit;
    using Xunit.Categories;

    public class SongTests
    {
        private readonly IAwesomeMusicContext _context;
        private readonly IIdentityUtility _identityUtility;
        private readonly IMapper _mapper;

        public SongTests()
        {
            _context = Substitute.For<IAwesomeMusicContext>();
            _identityUtility = Substitute.For<IIdentityUtility>();
            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile<SongProfile>();
                opts.AddProfile<UserProfile>();
            });
            _mapper = config.CreateMapper();
        }

        [Theory]
        [UnitTest("Get all Songs")]
        [JsonFileData("Songs.json", typeof(List<Song>))]
        public async Task SongQueryHandlers_GetAllSongsQueryHandler_GetAllSongs(List<Song> songs)
        {
            // Arrange
            var songsDbSetMock = songs.AsQueryable().BuildMockDbSet();
            var getAllSongsQueryDto = new GetAllSongsQuery();
            var currentUserId = "1";

            _context.Songs.Returns(songsDbSetMock);
            _identityUtility.GetNameIdentifier().Returns(currentUserId);

            // Act
            var handler = new SongQueryHandlers.GetAllSongsQueryHandler(_context, _identityUtility, _mapper);
            var response = await handler.Handle(getAllSongsQueryDto, new CancellationToken());

            // Assert
            response.Result.Should().NotBeNull();
            response.Result.TotalRecordCount.Should().Be(songsDbSetMock.Count());
            response.Result.Results.Should().HaveCount(songsDbSetMock.Count());
        }

        [Theory]
        [UnitTest("Get only private Songs")]
        [JsonFileData("Songs.json", typeof(List<Song>))]
        public async Task SongQueryHandlers_GetAllSongsQueryHandler_GetOnlyPrivateSongs(List<Song> songs)
        {
            // Arrange
            var songsDbSetMock = songs.AsQueryable().BuildMockDbSet();
            var getAllSongsQueryDto = new GetAllSongsQuery { IncludePublic = false };
            var currentUserId = "1";
            var expectedSongs = songs.Where(s => s.RegisteredById == int.Parse(currentUserId)).ToList();

            _context.Songs.Returns(songsDbSetMock);
            _identityUtility.GetNameIdentifier().Returns(currentUserId);

            // Act
            var handler = new SongQueryHandlers.GetAllSongsQueryHandler(_context, _identityUtility, _mapper);
            var response = await handler.Handle(getAllSongsQueryDto, new CancellationToken());

            // Assert
            response.Result.Should().NotBeNull();
            response.Result.TotalRecordCount.Should().Be(expectedSongs.Count);
            response.Result.Results.Should().HaveCount(expectedSongs.Count);
        }
    }
}
