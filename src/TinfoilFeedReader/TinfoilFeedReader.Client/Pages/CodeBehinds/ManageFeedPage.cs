using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Module.Feeds.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TinfoilFeedReader.Client.Infrastructure;

namespace TinfoilFeedReader.Client.Pages.CodeBehinds
{
    public class ManageFeedPage : ComponentBase
    {
        [Inject]
        public IDataAccess Service { get; set; }

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
        public FeedCollection Collection { get; private set; }

        protected async Task ToggleEdit()
        {
            if (IsEditing)
            {
                await Service.UpdateFeed(Collection, Feed);
            }

            IsEditing = !IsEditing;
        }

        protected async Task DeleteFeed()
        {
            if (await JsRuntime.InvokeAsync<bool>("confirm", $"{Feed.Name} will be removed."))
            {
                await Service.DeleteFeed(Collection, Feed)
                    .ConfigureAwait(false);

                UriHelper.NavigateTo($"feedcollections/{Collection.Id}/00000000-0000-0000-0000-000000000000");
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            Collection = await Service.GetFeedCollection(FeedCollectionId)
                .ConfigureAwait(false);

            Feed = Collection?.Feeds?.FirstOrDefault(feed => feed.Id == FeedId);
        }
    }
}
