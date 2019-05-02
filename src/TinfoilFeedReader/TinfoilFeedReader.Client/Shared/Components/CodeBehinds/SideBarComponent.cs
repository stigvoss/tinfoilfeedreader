using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinfoilFeedReader.Client.Shared.Components.CodeBehinds
{
    public class SideBarComponent : ComponentBase
    {
        bool collapseNavMenu = true;

        protected string NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        [Parameter]
        protected RenderFragment ChildContent { get; set; }

        protected void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }
    }
}
