using Microsoft.AspNetCore.Components;
using Module.Feeds.Domain;
using Module.Feeds.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinfoilFeedReader.Client.Shared.Components.CodeBehinds
{
    public class FeedBoxComponent : ComponentBase
    {
        [Parameter]
        public FeedEntry FeedEntry { get; set; }

        public string ImageData { get; set; }

        [Inject]
        protected ImageLoadService ImageLoader { get; set; }

        protected override async Task OnInitAsync()
        {
            //if (FeedEntry.ImageUrl is object)
            //{
            //    ImageData = await ImageLoader.ImageAsBase64From(FeedEntry.ImageUrl);
            //}
        }
    }
}
