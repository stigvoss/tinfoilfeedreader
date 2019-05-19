using Microsoft.AspNetCore.Components;
using Module.Feeds.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TinfoilFeedReader.Client.Infrastructure;

namespace TinfoilFeedReader.Client.Pages.CodeBehinds
{
    public class FeedCollectionsPage : ComponentBase
    {
        [Parameter]
        protected Guid? FeedCollectionId { get; set; }

        [Parameter]
        protected Guid? FeedId { get; set; }

        [Inject]
        protected IDataAccess Service { get; set; }

        protected IEnumerable<Article> Articles { get; set; }

        protected FeedCollection Collection { get; set; }

        protected string Title { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            Collection = await Service
                .GetFeedCollection(FeedCollectionId)
                .ConfigureAwait(false);

            var feed = Collection?.Feeds?
                .FirstOrDefault(e => e.Id == FeedId);

            Title = feed?.Name ?? "All";

            Articles = await Service
                .GetArticles(Collection, feed)
                .ConfigureAwait(false);
        }
    }
}
