using AutoMapper;
using DSMS.Application.Models.Appointment;
using DSMS.Application.Models.Enrollments;
using DSMS.Core.Entities;

namespace DSMS.Application.MappingProfiles
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile() {
            CreateMap<AppointmentModel, Appointment>();
            CreateMap<Appointment, AppointmentResponseModel>();
        }
    }
}
