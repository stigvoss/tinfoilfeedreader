using Microsoft.AspNetCore.Components;
using Module.Feeds.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TinfoilFeedReader.Client.Pages.CodeBehinds
{
    public class FeedCollectionsComponent : ComponentBase
    {
        [Parameter]
        protected string FeedCollectionId { get; set; }

        [Parameter]
        protected string FeedId { get; set; }

        [Inject]
        protected HttpClient Http { get; set; }

        protected IEnumerable<FeedEntry> Entries { get; set; }

        protected FeedCollection Collection { get; set; }

        protected string Title { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (FeedCollectionId is object)
            {
                Collection = await Http.GetJsonAsync<FeedCollection>($"api/feedcollection/{FeedCollectionId}")
                    .ConfigureAwait(false);

                if (FeedId is object)
                {
                    Title = Collection?.Feeds?.First(e => e.Id == Guid.Parse(FeedId))?.Name;
                }
                else
                {
                    Title = "All";
                }
            }

            if (FeedCollectionId is object && FeedId is object)
            {
                Entries = await Http.GetJsonAsync<FeedEntry[]>($"api/feedcollection/{FeedCollectionId}/feed/{FeedId}/entries")
                    .ConfigureAwait(false);
            }
            else if (FeedCollectionId is object)
            {
                Entries = await Http.GetJsonAsync<FeedEntry[]>($"api/feedcollection/{FeedCollectionId}/entries")
                    .ConfigureAwait(false);
            }
        }
    }
}
