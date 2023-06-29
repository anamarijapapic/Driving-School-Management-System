using AutoMapper;
using DSMS.Application.Models.Appointment;
using DSMS.Core.Entities;
using DSMS.Core.Entities.Identity;
using DSMS.Core.Enums;
using DSMS.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DSMS.Application.Services.Impl
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IMapper _mapper;

        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IMapper mapper, IAppointmentRepository appointmentRepository)
        {
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<AppointmentResponseModel> CreateAsync(CreateAppointmentModel createAppointmentModel)
        {
            var appointment = _mapper.Map<Appointment>(createAppointmentModel);

            return new AppointmentResponseModel
            {
                Id = (await _appointmentRepository.AddAsync(appointment)).Id
            };
        }

        public async Task<IEnumerable<AppointmentResponseModel>> GetAllAsync()
        {
            var appointments = await _appointmentRepository.GetAll().ToListAsync();

            return _mapper.Map<IEnumerable<AppointmentResponseModel>>(appointments);
        }

        public async Task<IEnumerable<AppointmentResponseModel>> GetByInstructorAsync(ApplicationUser instructor)
        {
            var appointments = await _appointmentRepository.GetByInstructor(instructor).ToListAsync();

            return _mapper.Map<IEnumerable<AppointmentResponseModel>>(appointments);
        }

        public async Task<IEnumerable<AppointmentResponseModel>> GetByStudentAsync(ApplicationUser student)
        {
            var appointments = await _appointmentRepository.GetByStudent(student).ToListAsync();

            return _mapper.Map<IEnumerable<AppointmentResponseModel>>(appointments);
        }

        public async Task<Appointment> GetByIdAsync(string id)
        {
            var appointment = (await _appointmentRepository.GetAllAsync(a => a.Id.ToString() == id)).FirstOrDefault();

            return appointment;
        }

        public async Task<IEnumerable<TimeOnly>> GetReservedSlotsByInstructorAndDateAsync(ApplicationUser instructor,
            DateOnly date)
        {
            var appointments = await _appointmentRepository.GetReservedSlotsByInstructorAndDate(instructor, date).ToListAsync();

            return appointments;
        }

        public async Task<Appointment> UpdateAsync(Appointment appointment)
        {
            return await _appointmentRepository.UpdateAsync(appointment);
        }

        public async Task AppointmentsToCompleteAsync()
        {
            var appointments = await _appointmentRepository.GetAll().ToListAsync();
            foreach (var appointment in appointments)
            {
                if (appointment.Status == AppointmentStatus.Reserved
                    && appointment.Date <= DateOnly.FromDateTime(DateTime.Now)
                    && appointment.End <= TimeOnly.FromDateTime(DateTime.Now))
                {
                    appointment.Status = AppointmentStatus.Completed;
                    await _appointmentRepository.UpdateAsync(appointment);
                }
            }
        }

        public IEnumerable<AppointmentResponseModel> Search(IEnumerable<AppointmentResponseModel> appointments, string searchString)
        {
            IEnumerable<AppointmentResponseModel> searchedAppointments = appointments;

            if (!string.IsNullOrEmpty(searchString))
            {
                var searchStringTrim = searchString.ToUpper().Trim();
                searchedAppointments = appointments
                    .Where(a => a.Instructor.FirstName.ToUpper().Contains(searchStringTrim) ||
                    a.Instructor.LastName.ToUpper().Contains(searchStringTrim) ||
                    a.Student.FirstName.ToUpper().Contains(searchStringTrim) ||
                    a.Student.LastName.ToUpper().Contains(searchStringTrim));
            }

            return searchedAppointments;
        }

        public IEnumerable<AppointmentResponseModel> Filter(IEnumerable<AppointmentResponseModel> appointments, string currentFilter)
        {
            IEnumerable<AppointmentResponseModel> filteredAppointments = appointments;

            if (!string.IsNullOrEmpty(currentFilter))
            {
                if (currentFilter == Times.Past.ToString())
                {
                    filteredAppointments = appointments
                        .Where(a => (a.Date < DateOnly.FromDateTime(DateTime.Now))
                            || (a.Date == DateOnly.FromDateTime(DateTime.Now)
                                && a.End < TimeOnly.FromDateTime(DateTime.Now)));
                }
                else if (currentFilter == Times.Future.ToString())
                {
                    filteredAppointments = appointments
                        .Where(a => (a.Date > DateOnly.FromDateTime(DateTime.Now))
                            || (a.Date == DateOnly.FromDateTime(DateTime.Now)
                                && a.Start > TimeOnly.FromDateTime(DateTime.Now)));
                }
            }

            return filteredAppointments;
        }

    }
}
