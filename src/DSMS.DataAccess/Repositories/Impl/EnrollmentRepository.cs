using DSMS.Core.Entities;
using DSMS.Core.Entities.Identity;
using DSMS.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DSMS.DataAccess.Repositories.Impl
{
    public class EnrollmentRepository : BaseRepository<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(DatabaseContext context) : base(context) { }

        public async Task<IEnumerable<Enrollment>> GetAllAsync()
        {
            return await DbSet
                .Include(e => e.Instructor)
                .Include(e => e.Student)
                .ToListAsync();
        }

        public async Task<IEnumerable<Enrollment>> GetByStudentAsync(ApplicationUser student)
        {
            return await DbSet
                .Include(e => e.Student)
                .Include(e => e.Instructor)
                .Where(e => e.Student == student)
                .ToListAsync();
        }

        public Enrollment GetById(string id)
        {
            return DbSet
                .Include(a => a.Instructor)
                .Include(a => a.Student)
                .Where(a => a.Id.ToString() == id)
                .FirstOrDefault();
        }
    }
}
