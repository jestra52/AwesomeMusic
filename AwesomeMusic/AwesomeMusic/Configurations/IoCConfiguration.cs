namespace AwesomeMusic.Configurations
{
    using AwesomeMusic.Data.Model;
    using AwesomeMusic.Middleware;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;

    public static class IoCConfiguration
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IAwesomeMusicContext, AwesomeMusicContext>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationMiddleware<,>));
        }
    }
}
