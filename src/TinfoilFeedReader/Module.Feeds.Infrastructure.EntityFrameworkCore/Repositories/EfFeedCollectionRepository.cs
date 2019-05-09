using Module.Feeds.Domain;
using Module.Feeds.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Module.Feeds.Infrastructure.EntityFrameworkCore.Repositories
{
    public class EfFeedCollectionRepository : IRepository<FeedCollection>
    {
        private readonly FeedCollectionContext _context;

        public EfFeedCollectionRepository(FeedCollectionContext context)
        {
            _context = context;
        }

        public void Add(FeedCollection item)
        {
            _context.Collections.Add(item);
            _context.SaveChanges();
        }

        public IEnumerable<FeedCollection> All()
        {
            throw new NotSupportedException();
        }

        public void Dispose()
        {
        }

        public void Remove(FeedCollection item)
        {
            _context.Collections.Remove(item);
            _context.SaveChanges();
        }

        public FeedCollection Single(Guid id)
        {
            return _context.Collections.SingleOrDefault(e => e.Id == id);
        }

        public void Update(FeedCollection item)
        {
            _context.Collections.Update(item);
            _context.SaveChanges();
        }
    }
}
