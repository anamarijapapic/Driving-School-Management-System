#nullable disable

using DSMS.Core.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSMS.Frontend.Pages.Users
{
    [Authorize(Roles = ("Administrator"))]
    public class DeleteModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public string Username { get; set; }

        public async Task<IActionResult> OnGetAsync(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return base.BadRequest($"Unable to load user with ID '{Id}'.");
            }

            var userName = await _userManager.GetUserNameAsync(user);
            Username = userName;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return base.BadRequest($"Unable to load user with ID '{Id}'.");
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred deleting user.");
            }

            return Redirect("~/Users/Index");
        }
    }
}
