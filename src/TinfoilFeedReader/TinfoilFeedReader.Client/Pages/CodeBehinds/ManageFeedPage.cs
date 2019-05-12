using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Module.Feeds.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TinfoilFeedReader.Client.Pages.CodeBehinds
{
    public class ManageFeedPage : ComponentBase
    {
        [Inject]
        public HttpClient Http { get; set; }

        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        [Inject]
        public IUriHelper UriHelper { get; set; }

        [Parameter]
        protected Guid? FeedCollectionId { get; set; }

        [Parameter]
        protected Guid? FeedId { get; set; }

        protected bool IsEditing { get; set; }

        public Feed Feed { get; private set; }

        protected async Task ToggleEdit()
        {
            if (IsEditing)
            {
                await Http
                    .PutJsonAsync($"api/feedcollections/{FeedCollectionId}/feed", Feed)
                    .ConfigureAwait(false);
            }

            IsEditing = !IsEditing;
        }

        protected async Task DeleteFeed()
        {
            if (await JsRuntime.InvokeAsync<bool>("confirm", $"{Feed.Name} will be removed."))
            {
                await Http
                    .DeleteAsync($"api/feedcollections/{FeedCollectionId}/feed/{Feed.Id}")
                    .ConfigureAwait(false);

                UriHelper.NavigateTo($"feedcollections/{FeedCollectionId}");
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            var collection = await Http
                .GetJsonAsync<FeedCollection>($"api/feedcollections/{FeedCollectionId.Value}")
                .ConfigureAwait(false);

            Feed = collection.Feeds.FirstOrDefault(feed => feed.Id == FeedId);
        }
    }
}
