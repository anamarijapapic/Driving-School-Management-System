#nullable disable

using DSMS.Application.Models.Enrollments;
using DSMS.Application.Models.User;
using DSMS.Application.Services;
using DSMS.Core.Entities;
using DSMS.Core.Entities.Identity;
using DSMS.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSMS.Frontend.Pages.Enrollments
{
    [Authorize(Roles = ("Administrator"))]
    public class EditModel : PageModel
    {
        private readonly IEnrollmentService _enrollmentService;

        private readonly IUserService _userService;

        private readonly UserManager<ApplicationUser> _userManager;

        public IEnumerable<UserResponseModel> Instructors { get; set; } = new List<UserResponseModel>();
        public IEnumerable<UserResponseModel> Students { get; set; } = new List<UserResponseModel>();

        public EditModel(IEnrollmentService enrollmentService,
            IUserService userService,
            UserManager<ApplicationUser> userManager)
        {
            _enrollmentService = enrollmentService;
            _userService = userService;
            _userManager = userManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public UpdateEnrollmentModel Input { get; set; }

        private async Task LoadAsync(Enrollment enrollment)
        {
            Instructors = await _userService.GetAllUsersByRoleAsync(ApplicationRole.Instructor);
            Students = await _userService.GetAllUsersByRoleAsync(ApplicationRole.Student);

            var instructorId = enrollment.Instructor?.Id;
            var category = enrollment.Category;
            var studentId = enrollment.Student?.Id;

            Input = new UpdateEnrollmentModel
            {
                InstructorId = instructorId,
                Category = category,
                StudentId = studentId
            };
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var enrollment = await _enrollmentService.GetByIdAsync(id);
            if (enrollment == null)
            {
                return base.BadRequest($"Unable to load enrollment with ID '{id}'.");
            }

            await LoadAsync(enrollment);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            var enrollment = await _enrollmentService.GetByIdAsync(id);
            if (enrollment == null)
            {
                return base.BadRequest($"Unable to load enrollment with ID '{id}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(enrollment);
                return Page();
            }

            var instructorId = enrollment.Instructor?.Id;
            if (Input.InstructorId != instructorId)
            {
                var instructor = await _userManager.FindByIdAsync(Input.InstructorId);
                enrollment.Instructor = instructor;
            }

            var studentId = enrollment.Student?.Id;
            if (Input.StudentId != studentId)
            {
                var student = await _userManager.FindByIdAsync(Input.StudentId);
                enrollment.Student = student;
            }

            var category = enrollment.Category;
            if (Input.Category != category)
            {
                enrollment.Category = Input.Category;
            }

            await _enrollmentService.UpdateAsync(enrollment);

            StatusMessage = "Enrollment details have been updated";

            return Page();
        }
    }
}
