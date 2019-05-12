using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Module.Feeds.Domain;
using Module.Feeds.Domain.Base;
using Module.Feeds.Infrastructure.EntityFrameworkCore;
using Module.Feeds.Infrastructure.EntityFrameworkCore.Base;
using Module.Feeds.Infrastructure.EntityFrameworkCore.Repositories;
using Module.Feeds.Infrastructure.EntityFrameworkCore.Services;
using Newtonsoft.Json;
using Npgsql;
using System.Linq;

namespace TinfoilFeedReader.Server
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            var connectionString = Configuration.GetConnectionString("FeedConnection");

            services.AddDbContextPool<FeedContext>(options => options.UseNpgsql(connectionString));
            services.AddScoped<IEntityReplaceService, EntityReplaceService>();
            services.AddScoped<IRepository<FeedCollection>, FeedCollectionsRepository>();
            services.AddScoped<ISourcesRepository, SourcesRepository>();

            services.AddResponseCaching();
        }

        private string GetConnectionString(IConfiguration configuration)
        {
            return new NpgsqlConnectionStringBuilder
            {
                Host = configuration.GetValue<string>("DB_HOST"),
                Port = configuration.GetValue<int>("DB_PORT"),
                Username = configuration.GetValue<string>("DB_USER"),
                Password = configuration.GetValue<string>("DB_PASSWORD")
            }.ConnectionString;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var serviceScope = scopeFactory.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<FeedContext>();
                context.Database.Migrate();
            }

            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBlazorDebugging();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            app.UseBlazor<Client.Startup>();
        }
    }
}
