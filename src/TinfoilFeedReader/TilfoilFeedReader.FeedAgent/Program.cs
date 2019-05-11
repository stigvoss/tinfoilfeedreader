using Microsoft.EntityFrameworkCore;
using Module.Feeds.Infrastructure.EntityFrameworkCore;
using Module.Feeds.Infrastructure.EntityFrameworkCore.Base;
using Module.Feeds.Infrastructure.EntityFrameworkCore.Repositories;
using Module.Feeds.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace TilfoilFeedReader.FeedAgent
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = "dock02.voss.net",
                UserID = "sa",
                Password = "DevelopmentPassword01!",
                InitialCatalog = "Tinfoil"
            };

            var optionsBuilder = new DbContextOptionsBuilder<FeedContext>();

            optionsBuilder.UseSqlServer(builder.ConnectionString);

            var context = new FeedContext(optionsBuilder.Options);
            var sources = new SourcesRepository(context);

            foreach (var source in await sources.All().ToListAsync())
            {
                var articles = await FeedLoader.ArticlesFrom(source);

                foreach (var article in articles)
                {
                    source.Articles.Add(article);
                }

                await sources.Update(source);
            }

            Console.WriteLine();
        }
    }
}
