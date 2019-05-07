using Microsoft.AspNetCore.Components;
using Module.Feeds.Domain;
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
    }
}
