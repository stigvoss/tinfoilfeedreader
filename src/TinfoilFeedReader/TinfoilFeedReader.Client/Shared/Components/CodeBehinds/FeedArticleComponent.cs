﻿using Microsoft.AspNetCore.Components;
using Module.Feeds.Domain;
using Module.Feeds.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinfoilFeedReader.Client.Shared.Components.CodeBehinds
{
    public class FeedArticleComponent : ComponentBase
    {
        [Parameter]
        public Article Article { get; set; }

        public string ImageData { get; set; }

        [Inject]
        protected ImageLoadService ImageLoader { get; set; }
    }
}
