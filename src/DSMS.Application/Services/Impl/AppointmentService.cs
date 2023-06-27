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
            var vehicles = _appointmentRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<AppointmentResponseModel>>(vehicles);
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

        public async Task<Appointment> GetByIdAsync(string id)
        {
            var appointment = (await _appointmentRepository.GetAllAsync(a => a.Id.ToString() == id)).FirstOrDefault();

            return appointment;
        }

        public async Task<IEnumerable<TimeOnly>> GetReservedSlotsByInstructorAndDateAsync(ApplicationUser instructor,
            DateOnly date)
        {
            var appointments = await _appointmentRepository.GetReservedSlotsByInstructorAndDateAsync(instructor, date);

            return appointments;
        }

        public async Task<Appointment> UpdateAsync(Appointment appointment)
        {
            return await _appointmentRepository.UpdateAsync(appointment);
        }
    }
}
