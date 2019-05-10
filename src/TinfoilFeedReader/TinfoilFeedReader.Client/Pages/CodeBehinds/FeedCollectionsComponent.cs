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
        protected Guid? FeedCollectionId { get; set; }

        [Parameter]
        protected Guid? FeedId { get; set; }

        [Inject]
        protected HttpClient Http { get; set; }

        protected IEnumerable<Article> Articles { get; set; }

        protected FeedCollection Collection { get; set; }

        protected string Title { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (FeedCollectionId is object)
            {
                Collection = await Http.GetJsonAsync<FeedCollection>($"api/feedcollections/{FeedCollectionId.Value}")
                    .ConfigureAwait(false);

                if (FeedId is object)
                {
                    Title = Collection?.Feeds?.First(e => e.Id == FeedId.Value)?.Name;
                }
                else
                {
                    Title = "All";
                }
            }

            if (FeedCollectionId is object && FeedId is object)
            {
                Articles = await Http.GetJsonAsync<Article[]>($"api/feedcollections/{FeedCollectionId.Value}/feed/{FeedId.Value}/articles")
                    .ConfigureAwait(false);
            }
            else if (FeedCollectionId is object)
            {
                Articles = await Http.GetJsonAsync<Article[]>($"api/feedcollections/{FeedCollectionId.Value}/articles")
                    .ConfigureAwait(false);
            }
        }
    }
}
