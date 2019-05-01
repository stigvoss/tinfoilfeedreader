using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Module.Feeds;
using Module.Feeds.Domain;
using Module.Feeds.Domain.Base;
using Module.Feeds.Infrastructure.Repositories;
using Module.Feeds.Infrastructure.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TinfoilFeedReader.Server.Controllers
{
    [Route("api/[controller]")]
    public class FeedCollectionController : Controller
    {
        private readonly IRepository<FeedCollection> _collections;

        public FeedCollectionController(IRepository<FeedCollection> collections)
        {
            _collections = collections;
        }

        [HttpGet("{id}")]
        public FeedCollection Get(Guid id)
        {
            return _collections.Single(id);
        }

        [HttpGet("{id}/entries")]
        public async Task<IEnumerable<FeedEntry>> GetEntries(Guid id)
        {
            var collection = _collections.Single(id);
            return await FeedLoader.EntriesFrom(collection);
        }

        [HttpGet("{id}/feed/{feedId}/entries")]
        public async Task<IEnumerable<FeedEntry>> GetFeedEntries(Guid id, Guid feedId)
        {
            var feed = _collections.Single(id)?.Feeds
                .FirstOrDefault(e => e.Id == feedId);
            return await FeedLoader.EntriesFrom(feed);
        }

        [HttpPut]
        public void Put([FromBody]FeedCollection collection)
        {
            _collections.Update(collection);
        }

        [HttpPost]
        public void Post([FromBody]FeedCollection collection)
        {
            _collections.Add(collection);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            var collection = _collections.Single(id);
            _collections.Remove(collection);
        }
    }
}
