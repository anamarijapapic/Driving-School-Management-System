using AutoMapper;
using DSMS.Application.Models.Appointment;
using DSMS.Application.Models.Vehicle;
using DSMS.Core.Entities;
using DSMS.Core.Enums;
using DSMS.Core.Entities.Identity;
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
            var vehicles = await _appointmentRepository.GetAll().ToListAsync();

            return _mapper.Map<IEnumerable<AppointmentResponseModel>>(vehicles);
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

        public async Task<Appointment> AppointmentToCompleteAsync(AppointmentResponseModel appointment)
        {
            Appointment appointment1 = new Appointment()
            {
                Id = appointment.Id,
                Date= appointment.Date,
                End= appointment.End,
                Instructor= appointment.Instructor,
                Start= appointment.Start,
                Status= Core.Enums.AppointmentStatus.Completed,
                Student = appointment.Student,
            };
            return await _appointmentRepository.UpdateAsync(appointment1);
        }

        public IEnumerable<AppointmentResponseModel> Search(IEnumerable<AppointmentResponseModel> appointments, string searchString)
        {
            IEnumerable<AppointmentResponseModel> searchedAppointments = appointments;

            if (!string.IsNullOrEmpty(searchString))
            {
                var searchStringTrim = searchString.ToLower().Trim();
                searchedAppointments = appointments.Where(a => 
                    a.Instructor.FirstName.ToLower().Contains(searchStringTrim) ||
                    a.Instructor.LastName.ToLower().Contains(searchStringTrim) ||
                    a.Student.FirstName.ToLower().Contains(searchStringTrim) ||
                    a.Student.LastName.ToLower().Contains(searchStringTrim));
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
                    filteredAppointments = appointments.Where(a => a.Date < DateOnly.FromDateTime(DateTime.Now));
                }
                else if (currentFilter == Times.Future.ToString())
                {
                    filteredAppointments = appointments.Where(a => a.Date > DateOnly.FromDateTime(DateTime.Now));
                }
            }

            return filteredAppointments;
        }

    }
}
