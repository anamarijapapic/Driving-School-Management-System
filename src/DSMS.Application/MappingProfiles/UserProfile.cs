using AutoMapper;
using DSMS.Application.Models.User;
using DSMS.Core.Entities.Identity;

namespace DSMS.Application.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserModel, ApplicationUser>();
        CreateMap<ApplicationUser, UserResponseModel>();
    }
}
