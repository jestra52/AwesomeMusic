namespace AwesomeMusic.Configurations
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;

    public static class MediatorConfiguration
    {
        private static readonly List<string> AssemblyList = new()
        {
            "AwesomeMusic.Data",
            "AwesomeMusic.Services"
        };

        public static void AddMediatRConfiguration(this IServiceCollection services)
        {
            foreach (var assembly in AssemblyList.Select(Assembly.Load))
            {
                services.AddMediatR(assembly);
            }
        }
    }
}
