#nullable disable

using DSMS.Core.Entities.Identity;
using DSMS.DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSMS.Frontend.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public DetailsModel(IUserRepository userRepository,
            UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public ApplicationUser ApplicationUser { get; private set; }
        public string UserRole { get; private set; }

        public async Task<IActionResult> OnGetAsync(string Id)
        {
            var user = await _userRepository.GetByIdAsync(Id);
            if (user == null)
            {
                return base.NotFound($"Unable to load user with ID '{Id}'.");
            }

            ApplicationUser = user;

            var roles = await _userManager.GetRolesAsync(user);

            UserRole = roles.First();

            return Page();
        }
    }
}
