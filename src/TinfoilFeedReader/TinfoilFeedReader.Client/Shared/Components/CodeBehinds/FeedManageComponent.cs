using Microsoft.AspNetCore.Components;
using Module.Feeds.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TinfoilFeedReader.Client.Shared.Components.CodeBehinds
{
    public class FeedManageComponent : ComponentBase
    {
        [Parameter]
        public Feed Feed { get; set; }

        protected bool IsEditing { get; set; } = false;

        protected void ToggleEdit()
        {
            IsEditing = !IsEditing;
        }
    }
}
