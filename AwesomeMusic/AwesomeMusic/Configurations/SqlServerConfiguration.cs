namespace AwesomeMusic.Configurations
{
    using System;
    using AwesomeMusic.Data.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class SqlServerConfiguration
    {
        public static void AddSqlServer(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AwesomeMusicContext>(options =>
            {
                options.UseSqlServer(connectionString,
                    sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null);
                    });
            });
        }
    }
}
