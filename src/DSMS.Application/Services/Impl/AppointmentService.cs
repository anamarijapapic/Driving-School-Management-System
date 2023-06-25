using AutoMapper;
using DSMS.Application.Models.Appointment;
using DSMS.Application.Models.Vehicle;
using DSMS.Core.Entities;
using DSMS.Core.Entities.Identity;
using DSMS.DataAccess.Repositories;
using DSMS.DataAccess.Repositories.Impl;
using Microsoft.AspNetCore.Identity;

namespace DSMS.Application.Services.Impl
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public AppointmentService(IMapper mapper, IAppointmentRepository appointmentRepository, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
            _userManager = userManager;
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
            var vehicles = await _appointmentRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<AppointmentResponseModel>>(vehicles);
        }
    }
}
