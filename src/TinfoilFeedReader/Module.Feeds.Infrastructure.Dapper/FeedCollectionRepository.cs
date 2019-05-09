using Dapper;
using Module.Feeds.Domain;
using Module.Feeds.Domain.Base;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace Module.Feeds.Infrastructure.Dapper
{
    public class FeedCollectionRepository : IRepository<FeedCollection>
    {
        private readonly DbConnection _connection;

        public FeedCollectionRepository(DbConnection connection)
        {
            _connection = connection;
        }

        public void Add(FeedCollection item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FeedCollection> All()
        {
            throw new NotSupportedException();
        }

        public void Dispose()
        {
        }

        public void Remove(FeedCollection item)
        {
            throw new NotImplementedException();
        }

        public FeedCollection Single(Guid id)
        {
            var sql = $@"
                SELECT
	                *
                FROM [FeedCollection] feedCollection
                INNER JOIN [Feed] feed
                ON feedCollection.Id = feed.FeedCollectionId
                WHERE feedCollection.Id = @FeedCollectionId
                ;
            ";
            var parameters = new { FeedCollectionId = id };

            var collections = new Dictionary<Guid, FeedCollection>();

            return _connection.Query<FeedCollection, Feed, FeedCollection>(
                sql,
                (collection, feed) =>
                {
                    if (!collections.TryGetValue(collection.Id, out var entry))
                    {
                        entry = collection;
                        entry.Feeds = new List<Feed>();
                        collections.Add(collection.Id, collection);
                    }

                    entry.Feeds.Add(feed);

                    return entry;
                }, param: parameters, splitOn: "Id").FirstOrDefault();
        }

        public void Update(FeedCollection item)
        {
            throw new NotImplementedException();
        }
    }
}
