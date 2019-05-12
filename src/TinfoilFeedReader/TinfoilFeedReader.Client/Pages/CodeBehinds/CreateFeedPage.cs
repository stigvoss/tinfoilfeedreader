using Microsoft.AspNetCore.Components;
using Module.Feeds.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TinfoilFeedReader.Client.Pages.CodeBehinds
{
    public class CreateFeedPage : ComponentBase
    {
        [Inject]
        public HttpClient Http { get; set; }

        [Inject]
        public IUriHelper UriHelper { get; set; }

        [Parameter]
        public Guid? FeedCollectionId { get; set; }

        protected Feed Feed { get; set; } = new Feed();

        protected async Task CreateFeed()
        {
            await Http.PostJsonAsync($"api/feedcollections/{FeedCollectionId}/feed", Feed)
                .ConfigureAwait(false);

            UriHelper.NavigateTo($"feedcollections/{FeedCollectionId}");
        }
    }
}
