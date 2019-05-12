using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Module.Feeds.Domain;
using Module.Feeds.Domain.Base;
using Module.Feeds.Infrastructure.EntityFrameworkCore;
using Module.Feeds.Infrastructure.EntityFrameworkCore.Base;
using Module.Feeds.Infrastructure.EntityFrameworkCore.Services;
using Module.Feeds.Infrastructure.Services;

namespace TinfoilFeedReader.Server.Controllers
{
    [Route("api/[controller]")]
    public class FeedCollectionsController : Controller
    {
        private readonly IRepository<FeedCollection> _collections;
        private readonly ISourcesRepository _sources;
        private readonly IEntityReplaceService _updateService;

        public FeedCollectionsController(
            IRepository<FeedCollection> collections,
            ISourcesRepository sources,
            IEntityReplaceService updateService)
        {
            _collections = collections;
            _sources = sources;
            _updateService = updateService;
        }

        [HttpGet("{id}")]
        public async Task<FeedCollection> Get(Guid id)
        {
            return await _collections.Single(id);
        }

        [HttpGet("{id}/articles")]
        public async Task<IEnumerable<Article>> GetArticles(Guid id)
        {
            var collection = await _collections.Single(id);
            var sources = collection.Feeds
                .SelectMany(feed => feed.FeedSources
                    .Select(feedSource => feedSource.Source.Id));

            return _sources.All(sources)
                .SelectMany(source => source.Articles)
                .OrderByDescending(article => article.PublishDate);
        }

        [HttpGet("{id}/feed/{feedId}/articles")]
        public async Task<IEnumerable<Article>> GetArticles(Guid id, Guid feedId)
        {
            var collection = await _collections.Single(id);
            var feed = collection?.Feeds
                .FirstOrDefault(e => e.Id == feedId);

            var sources = feed.FeedSources
                .Select(feedSource => feedSource.Source.Id);

            return _sources.All(sources)
                .SelectMany(source => source.Articles)
                .OrderByDescending(article => article.PublishDate);
        }

        [HttpPut]
        public async Task Put([FromBody]FeedCollection collection)
        {
            await _collections.Update(collection);
        }

        [HttpPut("{collectionId}/feed")]
        public async Task PutFeed([FromBody]Feed feed, [FromRoute]Guid collectionId)
        {
            var collection = await _collections.Single(collectionId);
            var existing = collection.Feeds.Single(f => f.Id == feed.Id);

            _updateService.Replace(existing, feed);

            await _collections.Update(collection);
        }

        [HttpPost]
        public async Task Post([FromBody]FeedCollection collection)
        {
            await _collections.Add(collection);
        }

        [HttpPost("{collectionId}/feed")]
        public async Task Post([FromBody]Feed feed, [FromRoute]Guid collectionId)
        {
            var collection = await _collections.Single(collectionId);

            collection.Feeds.Add(feed);

            await _collections.Update(collection);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            var collection = await _collections.Single(id);
            await _collections.Remove(collection);
        }

        [HttpDelete("{collectionId}/feed/{feedId}")]
        public async Task Delete(Guid collectionId, Guid feedId)
        {
            var collection = await _collections.Single(collectionId);

            var feed = collection.Feeds.First(f => f.Id == feedId);
            collection.Feeds.Remove(feed);

            await _collections.Update(collection);
        }
    }
}
