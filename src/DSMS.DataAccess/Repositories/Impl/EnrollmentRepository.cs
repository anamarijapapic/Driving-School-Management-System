using DSMS.Core.Entities;
using DSMS.Core.Entities.Identity;
using DSMS.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DSMS.DataAccess.Repositories.Impl
{
    public class EnrollmentRepository : BaseRepository<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(DatabaseContext context) : base(context) { }

        public IQueryable<Enrollment> GetAll()
        {
            return DbSet
               .Include(e => e.Instructor)
               .Include(e => e.Student)
               .AsQueryable();
        }

        public IQueryable<Enrollment> GetByStudent(ApplicationUser student)
        {
            return DbSet
                .Include(e => e.Student)
                .Include(e => e.Instructor)
                .Where(e => e.Student == student)
                .AsQueryable();
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
