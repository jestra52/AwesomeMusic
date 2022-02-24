namespace AwesomeMusic.Configurations
{
    using AwesomeMusic.Data.Model;
    using AwesomeMusic.Middleware;
    using AwesomeMusic.Services.Utility;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static class IoCConfiguration
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IAwesomeMusicContext, AwesomeMusicContext>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IIdentityUtility, IdentityUtility>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationMiddleware<,>));
        }
    }
}
