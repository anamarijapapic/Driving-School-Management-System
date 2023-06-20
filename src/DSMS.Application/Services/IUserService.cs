using DSMS.Application.Models;
using DSMS.Application.Models.Enrollments;
using DSMS.Application.Models.User;
using DSMS.Core.Enums;

namespace DSMS.Application.Services;

public interface IUserService
{
    Task<BaseResponseModel> ChangePasswordAsync(Guid userId, ChangePasswordModel changePasswordModel);

    Task<ConfirmEmailResponseModel> ConfirmEmailAsync(ConfirmEmailModel confirmEmailModel);

    Task<CreateUserResponseModel> CreateAsync(CreateUserModel createUserModel);

    Task<IEnumerable<UserResponseModel>> GetAllAsync();

    Task<IEnumerable<UserResponseModel>> GetAllInstructorsAsync();

    Task<IEnumerable<UserResponseModel>> GetAllUsersByRoleAsync(ApplicationRole applicationRole);

    Task<LoginResponseModel> LoginAsync(LoginUserModel loginUserModel);
}
