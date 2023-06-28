using DSMS.Core.Entities;
using DSMS.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace DSMS.DataAccess.Repositories.Impl
{
    public class FeedbackRepository : BaseRepository<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(DatabaseContext context) : base(context) { }

        public IQueryable<Feedback> GetAll()
        {
            return DbSet
                .Include(f => f.Instructor)
                .Include(f => f.Student)
                .Include(f => f.Reactions)
                .AsQueryable();
        }

        public async Task<IEnumerable<Feedback>> GetAllAsync()
        {
            return await DbSet
                .Include(f => f.Instructor)
                .Include(f => f.Student)
                .Include(f => f.Reactions)
                .ToListAsync();
        }

        public async Task<Feedback> GetByIdAsync(string id)
        {
            return await DbSet
                .Include(f => f.Instructor)
                .Include(f => f.Student)
                .Include(f => f.Reactions)
                .FirstOrDefaultAsync(feedback => feedback.Id.ToString() == id);
        }
    }
}
