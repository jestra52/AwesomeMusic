namespace AwesomeMusic
{
    using Azure.Identity;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Azure.KeyVault;
    using Microsoft.Azure.Services.AppAuthentication;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Configuration.AzureKeyVault;
    using Microsoft.Extensions.Hosting;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    var settings = config.Build();
                    var vaultName = $"https://{settings["AwmVaultName"]}.vault.azure.net/";
                    var azureServiceTokenProvider = new AzureServiceTokenProvider();
                    var keyVaultClient = new KeyVaultClient(async (auth, resource, scope) =>
                    {
                        var credentials = new DefaultAzureCredential(false);
                        var token = await credentials.GetTokenAsync(
                            new Azure.Core.TokenRequestContext(new[] { "https://vault.azure.net/.default" }));
                        return token.Token;
                    });

                    config.AddAzureKeyVault(
                       vaultName, keyVaultClient, new DefaultKeyVaultSecretManager());
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseStartup<Startup>();
                });
    }
}
