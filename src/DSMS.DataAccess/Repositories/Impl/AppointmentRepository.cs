using DSMS.Core.Entities;
using DSMS.Core.Entities.Identity;
using DSMS.Core.Enums;
using DSMS.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DSMS.DataAccess.Repositories.Impl
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(DatabaseContext context) : base(context) { }

        private IQueryable<Appointment> GetAll()
        {
            return DbSet
             .Include(a => a.Instructor)
             .Include(a => a.Student)
             .OrderBy(a => a.Status)
                 .ThenBy(a => a.Date)
                     .ThenBy(a => a.Start)
             .AsQueryable();
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return await GetAll().ToListAsync();
        }

        private IQueryable<Appointment> GetByInstructor(ApplicationUser instructor)
        {
            return DbSet
                .Include(a => a.Instructor)
                .Include(a => a.Student)
                .Where(a => a.Instructor == instructor)
                .OrderBy(a => a.Status)
                    .ThenBy(a => a.Date)
                        .ThenBy(a => a.Start)
                .AsQueryable();
        }

        public async Task<IEnumerable<Appointment>> GetByInstructorAsync(ApplicationUser instructor)
        {
            return await GetByInstructor(instructor).ToListAsync();
        }

        private IQueryable<Appointment> GetByStudent(ApplicationUser student)
        {
            return DbSet
                .Include(a => a.Instructor)
                .Include(a => a.Student)
                .Where(a => a.Student == student)
                .OrderBy(a => a.Status)
                    .ThenBy(a => a.Date)
                        .ThenBy(a => a.Start)
                .AsQueryable();
        }

        public async Task<IEnumerable<Appointment>> GetByStudentAsync(ApplicationUser student)
        {
            return await GetByStudent(student).ToListAsync();
        }

        public Appointment GetById(string id)
        {
            return DbSet
                .Include(a => a.Instructor)
                .Include(a => a.Student)
                .Where(a => a.Id.ToString() == id)
                .FirstOrDefault();
        }

        private IQueryable<TimeOnly> GetReservedSlotsByInstructorAndDate(ApplicationUser instructor,
            DateOnly date)
        {
            return DbSet
                .Include(a => a.Instructor)
                .Include(a => a.Student)
                .Where(a => a.Instructor == instructor)
                .Where(a => a.Date == date)
                .Where(a => a.Status == AppointmentStatus.Reserved)
                .OrderBy(a => a.Date)
                    .ThenBy(a => a.Start)
                .Select(a => a.Start)
                .AsQueryable();
        }

        public async Task<IEnumerable<TimeOnly>> GetReservedSlotsByInstructorAndDateAsync(ApplicationUser instructor,
            DateOnly date)
        {
            
               return await GetReservedSlotsByInstructorAndDate(instructor, date).ToListAsync();
        }
    }
}
