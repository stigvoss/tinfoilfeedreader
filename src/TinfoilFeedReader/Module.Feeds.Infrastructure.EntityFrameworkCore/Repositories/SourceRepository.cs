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
    public class SourceRepository : ISourceRepository
    {
        private readonly FeedContext _context;

        public SourceRepository(FeedContext context)
        {
            _context = context;
        }

        public Task Add(Source item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Source>> All()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Source>> All(IEnumerable<Guid> ids)
        {
            return await Task.Run(() => 
                _context.Sources
                    .Include(source => source.Articles)
                    .Where(source => ids.Contains(source.Id)))
                .ConfigureAwait(false);
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

        public Task Update(Source item)
        {
            throw new NotImplementedException();
        }
    }
}
