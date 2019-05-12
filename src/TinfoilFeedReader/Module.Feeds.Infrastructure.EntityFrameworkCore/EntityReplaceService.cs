using Module.Feeds.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Module.Feeds.Infrastructure.EntityFrameworkCore
{
    public class EntityReplaceService
    {
        private readonly FeedContext _context;

        public EntityReplaceService(FeedContext context)
        {
            _context = context;
        }

        public void Replace<T>(T existing, T replacement)
            where T : IEntity<T>
        {
            _context.Entry(existing).CurrentValues.SetValues(replacement);
        }
    }
}
