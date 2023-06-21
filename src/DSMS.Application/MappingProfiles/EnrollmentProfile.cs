using AutoMapper;
using DSMS.Application.Models.Enrollments;
using DSMS.Core.Entities;

namespace DSMS.Application.MappingProfiles;

public class EnrollmentProfile : Profile
{
    public EnrollmentProfile()
    {
        CreateMap<CreateEnrollmentModel, Enrollment>();
        CreateMap<Enrollment, EnrollmentResponseModel>();
    }
}
