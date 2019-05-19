using Module.Feeds.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinfoilFeedReader.Client.Infrastructure
{
    public interface IDataAccess
    {
        Task<FeedCollection> GetFeedCollection(Guid? collectionId);

        Task<IEnumerable<Article>> GetArticles(FeedCollection collection, Feed feed);

        Task AddFeed(FeedCollection collection, Feed feed);

        Task UpdateFeed(FeedCollection collection, Feed feed);

        Task DeleteFeed(FeedCollection collection, Feed feed);
    }
}
