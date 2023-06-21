#nullable disable
using DSMS.Application.Models.Enrollments;
using DSMS.Application.Models.User;
using DSMS.Application.Services;
using DSMS.Core.Entities;
using DSMS.Core.Entities.Identity;
using DSMS.Core.Enums;
using DSMS.DataAccess.Repositories;
using DSMS.DataAccess.Repositories.Impl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace DSMS.Frontend.Pages.Enrollments
{
    [Authorize(Roles =("Administrator"))]
    public class EditModel : PageModel
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;

        public IEnumerable<UserResponseModel> Instructors { get; set; } = new List<UserResponseModel>();
        public IEnumerable<UserResponseModel> Students { get; set; } = new List<UserResponseModel>();

        public EditModel(IEnrollmentRepository enrollmentRepository, IUserService userService, UserManager<ApplicationUser> userManager)
        {
            _enrollmentRepository = enrollmentRepository;
            _userService = userService;
            _userManager = userManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public CreateEnrollmentModel Input { get; set; }

        private async Task LoadAsync(Enrollment enrollment)
        {
            Instructors = await _userService.GetAllUsersByRoleAsync(ApplicationRole.Instructor);
            Students = await _userService.GetAllUsersByRoleAsync(ApplicationRole.Student);

            var instructorId = enrollment.Instructor?.Id;
            var category = enrollment.Category;
            var studentId = enrollment.Student?.Id;

            Input = new CreateEnrollmentModel { InstructorId = instructorId,Category = category,StudentId = studentId};
        }

        public async Task<IActionResult> OnGetAsync(string Id)
        {
            var result = await _enrollmentRepository.GetAllAsync(e => e.Id.ToString() == Id);
            var enrollment = result.First();
            if (enrollment == null)
            {
                return base.NotFound($"Unable to load enrollment with ID '{Id}'.");
            }
            await LoadAsync(enrollment);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string Id)
        {
            var result = await _enrollmentRepository.GetAllAsync(e => e.Id.ToString() == Id);
            var enrollment = result.First();
            if (enrollment == null)
            {
                return base.NotFound($"Unable to load enrollment with ID '{Id}'.");
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

            await _enrollmentRepository.UpdateAsync(enrollment);

            StatusMessage = "Enrollment details have been updated";

            return Page();

        }
    }
}
