#nullable disable

using DSMS.Application.Models.Feedback;
using DSMS.Application.Services;
using DSMS.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSMS.Frontend.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private readonly IUserService _userService;

        private readonly IFeedbackService _feedbackService;

        private readonly UserManager<ApplicationUser> _userManager;

        public DetailsModel(IUserService userService,
            IFeedbackService feedbackService,
            UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _feedbackService = feedbackService;
            _userManager = userManager;
        }

        [BindProperty]
        public CreateFeedbackModel Input { get; set; }

        public ApplicationUser ApplicationUser { get; private set; }

        public string UserRole { get; private set; }

        private async Task LoadAsync(ApplicationUser user)
        {
            ApplicationUser = user;

            var roles = await _userManager.GetRolesAsync(user);

            UserRole = roles.First();
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return base.NotFound($"Unable to load user with ID '{id}'.");
            }

            await LoadAsync(user);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return base.NotFound($"Unable to load user with ID '{id}'.");
            }

            await LoadAsync(user);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var student = await _userManager.GetUserAsync(User);

            Input.InstructorId = ApplicationUser.Id;
            Input.StudentId = student.Id;
            Input.CreatedOn = DateTime.Now;
            Input.IsAnonymous = false;

            try
            {
                await _feedbackService.CreateAsync(Input);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Redirect("~/Users/Index");
        }
    }
}
