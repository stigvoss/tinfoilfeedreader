using Module.Feeds.Domain.Base;
using System;

namespace Module.Feeds.Domain
{
    public class FeedSource : AggregateRoot<FeedSource>
    {
        public FeedSource()
            : base()
        { }

        public FeedSource(Guid id)
            : base(id)
        { }

        public string Name { get; set; }

        public Uri Url { get; set; }
    }
}