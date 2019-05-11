using Microsoft.EntityFrameworkCore;
using Module.Feeds.Domain;
using Module.Feeds.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Feeds.Infrastructure.EntityFrameworkCore.Repositories
{
    public class FeedCollectionsRepository : IRepository<FeedCollection>
    {
        private readonly FeedContext _context;

        public FeedCollectionsRepository(FeedContext context)
        {
            _context = context;
        }

        public async Task Add(FeedCollection item)
        {
            await _context.Collections.AddAsync(item)
                .ConfigureAwait(false);
            await _context.SaveChangesAsync()
                .ConfigureAwait(false);
        }

        public IQueryable<FeedCollection> All()
        {
            throw new NotSupportedException();
        }

        public void Dispose()
        {
        }

        public async Task Remove(FeedCollection item)
        {
            _context.Collections.Remove(item);
            await _context.SaveChangesAsync()
                .ConfigureAwait(false);
        }

        public async Task<FeedCollection> Single(Guid id)
        {
            return await _context.Collections
                .Include(collection => collection.Feeds)
                    .ThenInclude(feed => feed.FeedSources)
                        .ThenInclude(feedSource => feedSource.Source)
                .SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task Update(FeedCollection item)
        {
            _context.Collections.Update(item);
            await _context.SaveChangesAsync()
                .ConfigureAwait(false);
        }
    }
}
