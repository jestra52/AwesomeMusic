namespace AwesomeMusic.Test
{
    using System;
    using System.Linq;
    using AutoMapper;
    using AwesomeMusic.Data.MapperConfiguration;
    using Xunit;
    using Xunit.Categories;

    public class GeneralTests
    {
        [Fact]
        [UnitTest("Check Automapper configuration is ok")]
        public void Autommaper_Assert_Configuration()
        {
            var config = new MapperConfiguration(opts =>
            {
                GetAutoMapperProfiles(opts);
            });

            config.AssertConfigurationIsValid();
        }

        private static void GetAutoMapperProfiles(IMapperConfigurationExpression configuration)
        {
            var profiles = typeof(SongProfile).Assembly.GetTypes().Where(a => typeof(Profile).IsAssignableFrom(a));
            foreach (var profile in profiles)
            {
                configuration.AddProfile(Activator.CreateInstance(profile) as Profile);
            }
        }
    }
}
