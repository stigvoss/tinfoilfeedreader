using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using Module.Feeds.Infrastructure.Services;
using TinfoilFeedReader.Client.Infrastructure;

namespace TinfoilFeedReader.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ImageLoadService>();
            services.AddScoped<IDataAccess, ServiceAccess>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
