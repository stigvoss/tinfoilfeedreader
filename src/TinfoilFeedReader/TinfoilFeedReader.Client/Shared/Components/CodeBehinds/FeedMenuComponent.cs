using Microsoft.AspNetCore.Components;
using Module.Feeds.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TinfoilFeedReader.Client.Infrastructure;

namespace TinfoilFeedReader.Client.Shared.Components.CodeBehinds
{
    public class FeedMenuComponent : ComponentBase
    {
        [Inject]
        public IDataAccess Service { get; set; }

        [Parameter]
        public FeedCollection Collection { get; set; }
    }
}
