using Microsoft.AspNetCore.Components;
using Module.Feeds.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TinfoilFeedReader.Client.Shared.Components.CodeBehinds
{
    public class FeedMenuComponent : ComponentBase
    {
        [Inject]
        public HttpClient Http { get; set; }

        [Parameter]
        public Guid? FeedCollectionId { get; set; }

        public FeedCollection Collection { get; set; }

        protected override async Task OnInitAsync()
        {
            if (FeedCollectionId is object)
            {
                Collection = await Http.GetJsonAsync<FeedCollection>($"api/feedcollections/{FeedCollectionId}");
            }
        }
    }
}
