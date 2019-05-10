using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Module.Feeds.Domain;
using Module.Feeds.Domain.Base;
using Module.Feeds.Infrastructure.EntityFrameworkCore;
using Module.Feeds.Infrastructure.EntityFrameworkCore.Base;
using Module.Feeds.Infrastructure.EntityFrameworkCore.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Data.SqlClient;
using System.Linq;

namespace TinfoilFeedReader.Server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddNewtonsoftJson(options => 
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            var builder = new SqlConnectionStringBuilder
            {
                DataSource = "dock02.voss.net",
                UserID = "sa",
                Password = "DevelopmentPassword01!",
                InitialCatalog = "Tinfoil"
            };

            services.AddDbContext<FeedContext>(options => options.UseSqlServer(builder.ConnectionString));
            services.AddScoped<IRepository<FeedCollection>, FeedCollectionRepository>();
            services.AddScoped<ISourceRepository, SourceRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
