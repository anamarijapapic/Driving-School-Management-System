using DSMS.Core.Entities;
using DSMS.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

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
                .ToListAsync();
        }

        public Task<Reaction> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
