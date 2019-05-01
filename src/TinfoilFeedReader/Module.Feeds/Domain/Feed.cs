using Module.Feeds.Domain.Base;
using System;
using System.Collections.Generic;

namespace Module.Feeds.Domain
{
    public class Feed : Entity<Feed>
    {
        public Feed()
            : base()
        { }

        public Feed(Guid id)
            : base(id)
        { }

        public string Name { get; set; }

        public ICollection<FeedSource> Sources { get; set; } = new List<FeedSource>();
    }
}
