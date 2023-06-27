using DSMS.Application.Models.User;
using DSMS.Application.Services;
using DSMS.Application.Services.Impl;
using DSMS.Core.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSMS.Frontend.Pages.Users
{
    public class IndexModel : PageModel
    {

        private readonly IUserService _userService;

        public IEnumerable<UserResponseModel> Users { get; private set; } = new List<UserResponseModel>();

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<PageResult> OnGetAsync(string searchString, string currentFilter)
        {
            Users = await _userService.GetAllAsync();
            ViewData["Keyword"] = searchString;
            Users = _userService.Search(Users, searchString);

            ViewData["CurrentFilter"] = currentFilter;
            Users = _userService.Filter(Users, currentFilter);

            return Page();
        }
    }
}
