using Microsoft.AspNetCore.Components;
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

        [Parameter]
        protected Guid? FeedCollectionId { get; set; }

        [Parameter]
        protected Guid? FeedId { get; set; }

        protected bool IsEditing { get; set; } = false;

        public Feed Feed { get; private set; }

        protected async Task ToggleEdit()
        {
            if (IsEditing)
            {
                await Http.PutJsonAsync($"api/feedcollections/{FeedCollectionId}/feed", Feed)
                    .ConfigureAwait(false);
            }

            IsEditing = !IsEditing;
        }

        protected override async Task OnParametersSetAsync()
        {
            var collection = await Http.GetJsonAsync<FeedCollection>($"api/feedcollections/{FeedCollectionId.Value}")
                .ConfigureAwait(false);
            Feed = collection.Feeds.FirstOrDefault(e => e.Id == FeedId);
        }
    }
}
