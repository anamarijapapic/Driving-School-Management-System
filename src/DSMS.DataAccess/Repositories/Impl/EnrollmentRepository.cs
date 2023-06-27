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
            return await GetAll().ToListAsync();
        }

        private IQueryable<Enrollment> GetAll()
        {
            return DbSet
               .Include(e => e.Instructor)
               .Include(e => e.Student)
               .AsQueryable();
        }

        private IQueryable<Enrollment> GetByStudent(ApplicationUser student)
        {
            return DbSet
                .Include(e => e.Student)
                .Include(e => e.Instructor)
                .Where(e => e.Student == student)
                .AsQueryable();
        }

        public async Task<IEnumerable<Enrollment>> GetByStudentAsync(ApplicationUser student)
        {
            return await GetByStudent(student).ToListAsync();
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
