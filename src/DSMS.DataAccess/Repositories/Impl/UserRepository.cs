using DSMS.Core.Entities.Identity;
using DSMS.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DSMS.DataAccess.Repositories.Impl
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<ApplicationUser> DbSet;

        public UserRepository(DatabaseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            DbSet = context.Set<ApplicationUser>();
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }
    }
}
