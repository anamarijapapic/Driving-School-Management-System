using DSMS.Application.Models.User;
using DSMS.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSMS.Frontend.Pages.Users
{
    [Authorize(Roles = ("Administrator"))]
    public class IndexModel : PageModel
    {

        private readonly IUserService _userService;

        public IEnumerable<UserIndexModel> Users { get; private set; } = new List<UserIndexModel>();

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public async Task OnGetAsync()
        {
            Users = await _userService.GetAllAsync();
        }
    }
}
