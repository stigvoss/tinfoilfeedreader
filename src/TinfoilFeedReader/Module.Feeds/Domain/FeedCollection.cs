using Module.Feeds.Domain.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Module.Feeds.Domain
{
    public class FeedCollection : AggregateRoot<FeedCollection>
    {
        public FeedCollection()
            : base()
        { }

        public FeedCollection(Guid id)
            : base(id)
        { }

        public virtual List<Feed> Feeds { get; set; } = new List<Feed>();
    }
}
