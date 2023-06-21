#nullable disable
using DSMS.Application.Models.Enrollments;
using DSMS.Application.Models.User;
using DSMS.Application.Services;
using DSMS.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSMS.Frontend.Pages.Enrollments
{
    [Authorize(Roles = ("Administrator"))]
    public class CreateModel : PageModel
    {
        private readonly IEnrollmentService _enrollmentService;
        private readonly IUserService _userService;

        public IEnumerable<UserResponseModel> Instructors { get; private set; } = new List<UserResponseModel>();
        public IEnumerable<UserResponseModel> Students { get; private set; } = new List<UserResponseModel>();

        public CreateModel(IEnrollmentService enrollmentService, IUserService userService)
        {
            _enrollmentService = enrollmentService;
            _userService = userService;
        }

        [BindProperty]
        public CreateEnrollmentModel Input { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Instructors = await _userService.GetAllUsersByRoleAsync(ApplicationRole.Instructor);
            Students = await _userService.GetAllUsersByRoleAsync(ApplicationRole.Student);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            try
            {
                await _enrollmentService.CreateAsync(Input);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Redirect("~/Enrollments/Index");
        }
    }
}
