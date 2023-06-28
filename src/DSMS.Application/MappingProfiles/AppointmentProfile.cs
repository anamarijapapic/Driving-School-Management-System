using AutoMapper;
using DSMS.Application.Models.Appointment;
using DSMS.Core.Entities;

namespace DSMS.Application.MappingProfiles
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<CreateAppointmentModel, Appointment>();
            CreateMap<Appointment, AppointmentResponseModel>();
            CreateMap<AppointmentResponseModel, Appointment>();
        }
    }
}
