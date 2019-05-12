using Microsoft.AspNetCore.Components;
using Module.Feeds.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TinfoilFeedReader.Client.Pages.CodeBehinds
{
    public class ManageFeedCollectionsComponent : ComponentBase
    {
        [Inject]
        public HttpClient Http { get; set; }

        [Parameter]
        protected Guid? FeedCollectionId { get; set; }

        public FeedCollection Collection { get; set; }

        protected override async Task OnInitAsync()
        {
            Collection = await Http.GetJsonAsync<FeedCollection>($"api/feedcollections/{FeedCollectionId.Value}")
                    .ConfigureAwait(false);
        }
    }
}
