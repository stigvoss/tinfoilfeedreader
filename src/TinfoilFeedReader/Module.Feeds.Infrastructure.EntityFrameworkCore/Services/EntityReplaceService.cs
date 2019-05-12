using Module.Feeds.Domain.Base;
using Module.Feeds.Infrastructure.EntityFrameworkCore.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Module.Feeds.Infrastructure.EntityFrameworkCore.Services
{
    public class EntityReplaceService : IEntityReplaceService
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
