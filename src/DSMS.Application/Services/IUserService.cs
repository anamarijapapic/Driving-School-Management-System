using DSMS.Application.Models;
using DSMS.Application.Models.User;

namespace DSMS.Application.Services;

public interface IUserService
{
    Task<BaseResponseModel> ChangePasswordAsync(Guid userId, ChangePasswordModel changePasswordModel);

    Task<ConfirmEmailResponseModel> ConfirmEmailAsync(ConfirmEmailModel confirmEmailModel);

    Task<CreateUserResponseModel> CreateAsync(CreateUserModel createUserModel);

    Task<IEnumerable<UserResponseModel>> GetAllAsync();

    Task<IEnumerable<UserResponseModel>> GetAllInstructorsAsync();

    Task<LoginResponseModel> LoginAsync(LoginUserModel loginUserModel);
}
