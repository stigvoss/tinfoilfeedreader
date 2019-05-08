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
        public static async Task<IEnumerable<FeedEntry>> EntriesFrom(FeedCollection collection)
        {
            var sources = collection.Feeds.SelectMany(e => e.Sources);
            return await EntriesFrom(sources);
        }

        public static async Task<IEnumerable<FeedEntry>> EntriesFrom(Feed feed)
        {
            return await EntriesFrom(feed.Sources);
        }

        public static async Task<IEnumerable<FeedEntry>> EntriesFrom(IEnumerable<FeedSource> feedSources)
        {
            var entries = new List<FeedEntry>();

            var client = new HttpClient();

            foreach (var feedSource in feedSources)
            {
                using (var stream = await client.GetStreamAsync(feedSource.Url))
                using (var reader = XmlReader.Create(stream))
                {
                    var feed = SyndicationFeed.Load(reader);

                    foreach (var item in feed.Items)
                    {
                        var feedEntry = (FeedEntry)item;

                        feedEntry.SourceName = feedSource.Name;

                        entries.Add(feedEntry);
                    }
                }
            }

            return entries.OrderByDescending(e => e.PublishDate);
        }
    }
}
