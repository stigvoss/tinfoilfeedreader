using Module.Feeds.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Module.Feeds.Infrastructure.Services
{
    public static class FeedLoader
    {
        public static async Task<IEnumerable<Article>> EntriesFrom(FeedCollection collection)
        {
            var sources = collection.Feeds.SelectMany(e => e.FeedSources);
            return await EntriesFrom(sources);
        }

        public static async Task<IEnumerable<Article>> EntriesFrom(Feed feed)
        {
            return await EntriesFrom(feed.FeedSources);
        }

        public static async Task<IEnumerable<Article>> EntriesFrom(IEnumerable<FeedSource> feedSources)
        {
            var entries = new List<Article>();

            var client = new HttpClient();

            foreach (var feedSource in feedSources)
            {
                using (var stream = await client.GetStreamAsync(feedSource.Source.Url))
                using (var reader = XmlReader.Create(stream))
                {
                    var feed = SyndicationFeed.Load(reader);

                    foreach (var item in feed.Items)
                    {
                        var feedEntry = (Article)item;

                        feedEntry.SourceName = feedSource.Name ?? feedSource.Source.Name;

                        entries.Add(feedEntry);
                    }
                }
            }

            return entries.OrderByDescending(e => e.PublishDate);
        }
    }
}
