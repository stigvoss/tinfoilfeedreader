using Microsoft.EntityFrameworkCore;
using Module.Feeds.Domain;
using Module.Feeds.Domain.Base;
using Module.Feeds.Infrastructure.EntityFrameworkCore.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Feeds.Infrastructure.EntityFrameworkCore.Repositories
{
    public class SourcesRepository : ISourcesRepository
    {
        private readonly FeedContext _context;

        public SourcesRepository(FeedContext context)
        {
            _context = context;
        }

        public Task Add(Source item)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Source> All()
        {
            return _context.Sources
                .Include(source => source.Articles);
        }

        public IQueryable<Source> All(IEnumerable<Guid> ids)
        {
            return _context.Sources
                .Include(source => source.Articles)
                .Where(source => ids.Contains(source.Id));
        }

        public void Dispose()
        {
        }

        public Task Remove(Source item)
        {
            throw new NotImplementedException();
        }

        public Task<Source> Single(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Source item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.Sources.Update(item);

            await _context.SaveChangesAsync();
        }
    }
}
