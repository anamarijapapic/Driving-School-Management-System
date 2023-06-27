using DSMS.Core.Entities;
using DSMS.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMS.DataAccess.Repositories.Impl
{
    public class ReactionRepository : BaseRepository<Reaction>, IReactionRepository
    {
        public ReactionRepository(DatabaseContext context) : base(context) { }

        public async Task<IEnumerable<Reaction>> GetAllAsync()
        {
            return await DbSet
                .Include(r => r.Student)
                .Include(r => r.Feedback)
                .ToListAsync<Reaction>();
        }

        public Task<Reaction> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
