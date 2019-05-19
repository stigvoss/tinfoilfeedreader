using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Module.Feeds.Domain;

namespace TinfoilFeedReader.Client.Infrastructure
{
    public class ServiceAccess : IDataAccess
    {
        private readonly HttpClient _http;

        public ServiceAccess(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<Article>> Articles(Feed feed)
        {
            if (feed is null)
            {
                return null;
            }

            return await _http.GetJsonAsync<Article[]>($"api/feed/{feed.Id}/articles")
                       .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Article>> GetArticles(FeedCollection collection, Feed feed)
        {
            if (collection is null)
            {
                return null;
            }

            var url = $"api/feedcollections/{collection.Id}/articles";

            if (feed is object)
            {
                url = $"api/feedcollections/{collection.Id}/feed/{feed.Id}/articles";
            }

            return await _http.GetJsonAsync<Article[]>(url)
                       .ConfigureAwait(false);
        }

        public async Task<FeedCollection> GetFeedCollection(Guid? id)
        {
            if (id is null)
            {
                return null;
            }

            return await _http
                .GetJsonAsync<FeedCollection>($"api/feedcollections/{id}")
                .ConfigureAwait(false);
        }

        public async Task AddFeed(FeedCollection collection, Feed feed)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (feed is null)
            {
                throw new ArgumentNullException(nameof(feed));
            }

            await _http
                .PostJsonAsync($"api/feedcollections/{collection.Id}/feed", feed)
                .ConfigureAwait(false);
        }

        public async Task UpdateFeed(FeedCollection collection, Feed feed)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (feed is null)
            {
                throw new ArgumentNullException(nameof(feed));
            }

            await _http
                .PutJsonAsync($"api/feedcollections/{collection.Id}/feed", feed)
                .ConfigureAwait(false);
        }

        public async Task DeleteFeed(FeedCollection collection, Feed feed)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (feed is null)
            {
                throw new ArgumentNullException(nameof(feed));
            }

            await _http
                .DeleteAsync($"api/feedcollections/{collection.Id}/feed/{feed.Id}")
                .ConfigureAwait(false);
        }
    }
}
