using AutoMapper;
using DSMS.Application.Models.Appointment;
using DSMS.Core.Entities;
using DSMS.Core.Entities.Identity;
using DSMS.DataAccess.Repositories;

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

        public async Task<AppointmentResponseModel> CreateAsync(AppointmentModel appointmentModel)
        {
            var appointment = _mapper.Map<Appointment>(appointmentModel);

            return new AppointmentResponseModel
            {
                Id = (await _appointmentRepository.AddAsync(appointment)).Id
            };
        }

        public async Task<IEnumerable<AppointmentResponseModel>> GetAllAsync()
        {
            var appointments = await _appointmentRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<AppointmentResponseModel>>(appointments);
        }

        public async Task<IEnumerable<AppointmentResponseModel>> GetByInstructorAsync(ApplicationUser instructor)
        {
            var appointments = await _appointmentRepository.GetByInstructorAsync(instructor);

            return _mapper.Map<IEnumerable<AppointmentResponseModel>>(appointments);
        }

        public async Task<IEnumerable<AppointmentResponseModel>> GetByStudentAsync(ApplicationUser student)
        {
            var appointments = await _appointmentRepository.GetByStudentAsync(student);

            return _mapper.Map<IEnumerable<AppointmentResponseModel>>(appointments);
        }

        public async Task<IEnumerable<TimeOnly>> GetReservedSlotsByInstructorAndDateAsync(ApplicationUser instructor,
            DateOnly date)
        {
            var appointments = await _appointmentRepository.GetReservedSlotsByInstructorAndDateAsync(instructor, date);

            return appointments;
        }
    }
}
