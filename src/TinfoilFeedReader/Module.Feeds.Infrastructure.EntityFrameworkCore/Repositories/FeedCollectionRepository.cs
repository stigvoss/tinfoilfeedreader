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
    public class FeedCollectionRepository : IRepository<FeedCollection>
    {
        private readonly FeedContext _context;

        public FeedCollectionRepository(FeedContext context)
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

        public Task<IEnumerable<FeedCollection>> All()
        {
            throw new NotSupportedException();
        }

        public void Dispose()
        {
        }

        public async Task Remove(FeedCollection item)
        {
            await Task.Run(() =>
            {
                _context.Collections.Remove(item);
                _context.SaveChanges();
            }).ConfigureAwait(false);
        }

        public async Task<FeedCollection> Single(Guid id)
        {
            return await _context.Collections
                .Include(collection => collection.Feeds)
                    .ThenInclude(feed => feed.FeedSources)
                        .ThenInclude(feedSource => feedSource.Source)
                .SingleOrDefaultAsync(e => e.Id == id)
                .ConfigureAwait(false);
        }

        public async Task Update(FeedCollection item)
        {
            await Task.Run(() =>
            {
                _context.Collections.Update(item);
                _context.SaveChanges();
            }).ConfigureAwait(false);
        }
    }
}
