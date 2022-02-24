namespace AwesomeMusic.Configurations
{
    using System;
    using System.Linq;
    using AutoMapper;
    using AwesomeMusic.Data.MapperConfiguration;
    using Microsoft.Extensions.DependencyInjection;

    public static class AutoMapperConfiguration
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => GetAutoMapperProfiles(cfg));
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
