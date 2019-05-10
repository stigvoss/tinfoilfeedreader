using Module.Feeds.Domain.Base;
using System;

namespace Module.Feeds.Domain
{
    public class FeedSource : IValueObject
    {
        public string Name { get; set; }

        public virtual Source Source { get; set; }
    }
}