using System;
using System.Linq;
using System.ServiceModel.Syndication;

namespace Module.Feeds.Domain
{
    public struct FeedEntry
    {
        public string Title { get; set; }

        public string Link { get; set; }

        public DateTimeOffset PublishDate { get; private set; }

        public static explicit operator FeedEntry(SyndicationItem item)
        {
            return new FeedEntry
            {
                Title = item.Title?.Text,
                Link = item.Links?.FirstOrDefault().Uri.AbsoluteUri,
                PublishDate = item.PublishDate
            };
        }
    }
}