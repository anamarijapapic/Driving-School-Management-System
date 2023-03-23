using AutoMapper;
using DSMS.Application.Models.User;
using DSMS.DataAccess.Identity;

namespace DSMS.Application.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserModel, ApplicationUser>();
    }
}
