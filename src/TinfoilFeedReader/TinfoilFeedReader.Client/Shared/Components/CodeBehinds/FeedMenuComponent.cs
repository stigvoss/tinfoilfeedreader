﻿using Microsoft.AspNetCore.Components;
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

        public FeedCollection Collection { get; private set; }

        public Guid FeedCollectionId { get; set; } = Guid.Parse("69040700-3b62-42cf-803e-0c26c40db842");

        protected override async Task OnInitAsync()
        {
            Collection = await Http.GetJsonAsync<FeedCollection>("api/feedcollection/69040700-3b62-42cf-803e-0c26c40db842");
        }
    }
}
