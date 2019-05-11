using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Module.Feeds.Infrastructure.EntityFrameworkCore;
using Module.Feeds.Infrastructure.EntityFrameworkCore.Base;
using Module.Feeds.Infrastructure.EntityFrameworkCore.Repositories;
using Module.Feeds.Infrastructure.Services;
using Npgsql;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace TilfoilFeedReader.FeedAgent
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureAppConfiguration(config => config
                    .AddCommandLine(args)
                    .AddJsonFile("appsettings.json")
                    .Build())
                .ConfigureServices((context, services) =>
                {
                    var connectionString = context.Configuration.GetConnectionString("DefaultConnection");

                    services.AddOptions();

                    services.AddDbContextPool<FeedContext>(options => options.UseNpgsql(connectionString));
                    services.AddScoped<ISourcesRepository, SourcesRepository>();
                    services.AddHostedService<FeedUpdateAgent>();
                })
                .ConfigureLogging(logging => logging.AddConsole());

            await builder.RunConsoleAsync();
        }

        class FeedUpdateAgent : IHostedService
        {
            private readonly ISourcesRepository _sources;
            private readonly ILogger<FeedUpdateAgent> _logger;
            private readonly CancellationTokenSource _cancellationTokenSource;
            private Task _scheduler;

            public FeedUpdateAgent(ISourcesRepository sources, ILogger<FeedUpdateAgent> logger)
            {
                _sources = sources;
                _logger = logger;
                _cancellationTokenSource = new CancellationTokenSource();
            }

            public Task StartAsync(CancellationToken cancellationToken)
            {
                _scheduler = ScheduledTask.Run(async () =>
                {
                    _logger.LogInformation("Initiating update...");

                    try
                    {
                        foreach (var source in await _sources.All().ToListAsync())
                        {
                            _logger.LogInformation($"Updating {source.Name}...");

                            var addedArticleCount = 0;
                            var articles = await FeedLoader.ArticlesFrom(source);

                            var urls = source.Articles
                                .OrderByDescending(article => article.PublishDate)
                                .Take(articles.Count * 2)
                                .Select(article => article.Url)
                                .ToHashSet();

                            foreach (var article in articles)
                            {
                                if (!urls.Contains(article.Url))
                                {
                                    source.Articles.Add(article);
                                    addedArticleCount++;
                                }
                            }

                            await _sources.Update(source);

                            _logger.LogInformation($"{source.Name} finished. {addedArticleCount} added.");
                        }

                        _logger.LogInformation("Update finished.");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Update failed.");
                    }
                }, TimeSpan.FromMinutes(5), _cancellationTokenSource.Token);

                return Task.CompletedTask;
            }

            public async Task StopAsync(CancellationToken cancellationToken)
            {
                _cancellationTokenSource.Cancel();
                await _scheduler;
            }
        }
        class ScheduledTask
        {
            public static async Task Run(Action action, TimeSpan interval, CancellationToken cancellationToken)
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    if (!cancellationToken.IsCancellationRequested)
                    {
                        action();
                    }

                    try
                    {
                        await Task.Delay(interval, cancellationToken);
                    }
                    catch (TaskCanceledException) { }
                }
            }

            public static Task Run(Action action, TimeSpan period)
            {
                return Run(action, period, CancellationToken.None);
            }
        }
    }
}
