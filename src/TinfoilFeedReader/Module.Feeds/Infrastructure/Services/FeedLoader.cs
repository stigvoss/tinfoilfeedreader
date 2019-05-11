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
        public static async Task<ICollection<Article>> ArticlesFrom(Source source)
        {
            var articles = new List<Article>();

            var client = new HttpClient();

            using (var stream = await client.GetStreamAsync(source.Url))
            using (var reader = XmlReader.Create(stream))
            {
                var feed = SyndicationFeed.Load(reader);

                foreach (var item in feed.Items)
                {
                    var article = (Article)item;
                    articles.Add(article);
                }
            }

            return articles;
        }
    }
}
