using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinfoilFeedReader.Client.Shared.Components.CodeBehinds
{
    public class SideBarComponent : ComponentBase
    {
        [Parameter]
        protected RenderFragment ChildContent { get; set; }
    }
}
