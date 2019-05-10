using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Module.Feeds.Domain;
using Module.Feeds.Domain.Base;
using Module.Feeds.Infrastructure.EntityFrameworkCore.Base;
using Module.Feeds.Infrastructure.Services;

namespace TinfoilFeedReader.Server.Controllers
{
    [Route("api/[controller]")]
    public class FeedCollectionsController : Controller
    {
        private readonly IRepository<FeedCollection> _collections;
        private readonly ISourceRepository _sources;

        public FeedCollectionsController(IRepository<FeedCollection> collections, ISourceRepository sources)
        {
            _collections = collections;
            _sources = sources;
        }

        [HttpGet("{id}")]
        public async Task<FeedCollection> Get(Guid id)
        {
            return await _collections.Single(id);
        }

        [HttpGet("{id}/entries")]
        public async Task<IEnumerable<Article>> GetEntries(Guid id)
        {
            var collection = await _collections.Single(id);
            var sources = collection.Feeds
                .SelectMany(feed => feed.FeedSources
                        .Select(feedSource => feedSource.Source.Id));

            return (await _sources.All(sources)).SelectMany(source => source.Articles);
        }

        [HttpGet("{id}/feed/{feedId}/entries")]
        public async Task<IEnumerable<Article>> GetFeedEntries(Guid id, Guid feedId)
        {
            var feed = (await _collections.Single(id))?.Feeds
                .FirstOrDefault(e => e.Id == feedId);

            var sources = feed.FeedSources.Select(feedSource => feedSource.Source.Id);

            return (await _sources.All(sources)).SelectMany(source => source.Articles);
        }

        [HttpPut]
        public async Task Put([FromBody]FeedCollection collection)
        {
            await _collections.Update(collection);
        }

        [HttpPost]
        public async Task Post([FromBody]FeedCollection collection)
        {
            await _collections.Add(collection);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            var collection = await _collections.Single(id);
            await _collections.Remove(collection);
        }
    }
}
