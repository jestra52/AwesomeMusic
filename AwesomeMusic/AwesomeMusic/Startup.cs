namespace AwesomeMusic
{
    using AwesomeMusic.Configurations;
    using AwesomeMusic.Middleware;
    using AwesomeMusic.Services.Validators.CommandValidators.UserValidators;
    using FluentValidation;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["AwesomeMusicDbConnectionString"];
            var secretKey = Configuration["SecretKey"];

            services.AddControllers();
            services.AddValidatorsFromAssembly(typeof(CreateUserCommandValidator).Assembly);
            services.AddSqlServer(connectionString);
            services.AddAutoMapperConfiguration();
            services.AddMediatRConfiguration();
            services.AddJwtAuthConfiguration(secretKey);
            services.AddSwaggerConfiguration();
            services.AddCustomServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AwesomeMusic v1"));
            app.UseAuthentication();
            app.UseGlobalExceptionHandler();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
