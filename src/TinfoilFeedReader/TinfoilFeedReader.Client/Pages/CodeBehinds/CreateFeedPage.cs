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
    public class CreateFeedPage : ComponentBase
    {
        [Inject]
        public IDataAccess Service { get; set; }

        [Inject]
        public IUriHelper UriHelper { get; set; }

        [Parameter]
        public Guid? FeedCollectionId { get; set; }

        public FeedCollection Collection { get; private set; }

        protected Feed Feed { get; set; } = new Feed();

        protected async Task CreateFeed()
        {
            await Service.AddFeed(Collection, Feed);

            UriHelper.NavigateTo($"feedcollections/{FeedCollectionId}/00000000-0000-0000-0000-000000000000");
        }

        protected override async Task OnParametersSetAsync()
        {
            Collection = await Service.GetFeedCollection(FeedCollectionId)
                .ConfigureAwait(true);
        }
    }
}
