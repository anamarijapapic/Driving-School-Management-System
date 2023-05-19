using DSMS.Core.Entities.Identity;
using DSMS.Core.Exceptions;
using DSMS.DataAccess.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

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
