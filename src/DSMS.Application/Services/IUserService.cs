using DSMS.Application.Models;
using DSMS.Application.Models.User;
using DSMS.Core.Entities.Identity;
using DSMS.Core.Enums;

namespace DSMS.Application.Services;

public interface IUserService
{
    Task<CreateUserResponseModel> CreateAsync(CreateUserModel createUserModel);

    Task<LoginResponseModel> LoginAsync(LoginUserModel loginUserModel);

    Task<ConfirmEmailResponseModel> ConfirmEmailAsync(ConfirmEmailModel confirmEmailModel);

    Task<BaseResponseModel> ChangePasswordAsync(Guid userId, ChangePasswordModel changePasswordModel);

    Task<IEnumerable<UserResponseModel>> GetAllAsync();

    Task<IEnumerable<UserResponseModel>> GetAllUsersByRoleAsync(ApplicationRole applicationRole);

    Task<ApplicationUser> GetByIdAsync(string id);

    IEnumerable<UserResponseModel> Search(IEnumerable<UserResponseModel> users, string searchString);

    IEnumerable<UserResponseModel> Filter(IEnumerable<UserResponseModel> users, string currentFilter);
}
