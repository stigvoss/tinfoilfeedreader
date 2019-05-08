using Module.Feeds.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;

namespace Module.Feeds.Domain
{
    public struct FeedEntry
    {
        public string Title { get; set; }

        public string SourceUrl { get; set; }

        public string SourceName { get; set; }

        public DateTimeOffset PublishDate { get; set; }

        public string TimeSincePublish()
        {
            var timeSince = DateTime.Now - PublishDate.LocalDateTime;

            if (timeSince < TimeSpan.FromMinutes(1))
            {
                return $"{timeSince.Seconds}s";
            }
            else if (timeSince < TimeSpan.FromHours(1))
            {
                return $"{timeSince.Minutes}m";
            }
            else if (timeSince < TimeSpan.FromDays(1))
            {
                return $"{timeSince.Hours}h";
            }

            return $"{timeSince.Days}d";
        }

        public IEnumerable<string> Authors { get; set; }

        public string Summary { get; set; }

        public string ImageUrl { get; set; }

        public static explicit operator FeedEntry(SyndicationItem item)
            => new FeedEntry
            {
                Title = item.Title?.Text,
                SourceUrl = item.Links?.FirstOrDefault().Uri.AbsoluteUri,
                PublishDate = item.PublishDate,
                Authors = item.Authors?.Select(e => e.Name),
                Summary = item.Summary?.Text,
                ImageUrl = ContentService.ReadImageUrl(item.Content)
            };
    }
}