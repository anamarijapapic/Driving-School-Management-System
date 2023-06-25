using DSMS.Core.Entities;
using DSMS.Core.Entities.Identity;
using DSMS.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DSMS.DataAccess.Repositories.Impl
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(DatabaseContext context) : base(context) { }

        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return await DbSet
                .Include(a => a.Instructor)
                .Include(a => a.Student)
                .OrderBy(a => a.Date)
                .OrderBy(a => a.Start)
                .ToListAsync();
        }
        public async Task<IEnumerable<Appointment>> GetByIdAsync(string id)
        {
            return await DbSet
                .Include(a => a.Instructor)
                .Include(a => a.Student)
                .Where(a => a.Id.ToString() == id)
                .OrderBy(a => a.Date)
                .OrderBy(a => a.Start)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetByInstructorAsync(ApplicationUser instructor)
        {
            return await DbSet
                .Include(a => a.Instructor)
                .Include(a => a.Student)
                .Where(a => a.Instructor == instructor)
                .OrderBy(a => a.Date)
                .OrderBy(a => a.Start)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetByStudentAsync(ApplicationUser student)
        {
            return await DbSet
                .Include(a => a.Instructor)
                .Include(a => a.Student)
                .Where(a => a.Student == student)
                .OrderBy(a => a.Date)
                .OrderBy(a => a.Start)
                .ToListAsync();
        }

        public async Task<IEnumerable<TimeOnly>> GetReservedSlotsByInstructorAndDateAsync(ApplicationUser instructor,
            DateOnly date)
        {
            return await DbSet
                .Include(a => a.Instructor)
                .Include(a => a.Student)
                .Where(a => a.Instructor == instructor)
                .Where(a => a.Date == date)
                .OrderBy(a => a.Date)
                .OrderBy (a => a.Start)
                .Select(a => a.Start)
                .ToListAsync();
        }
    }
}
