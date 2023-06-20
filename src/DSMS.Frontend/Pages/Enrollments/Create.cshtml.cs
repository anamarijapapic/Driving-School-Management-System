#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AutoMapper;
using DSMS.Application.Models.User;
using DSMS.Application.Models.Enrollments;
using DSMS.Application.Services;
using DSMS.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using DSMS.Core.Enums;

namespace DSMS.Frontend.Pages.Enrollments
{
    [Authorize(Roles = ("Administrator"))]
    public class CreateModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly IEnrollmentService _enrollmentService;
        private readonly IUserService _userService;

        public IEnumerable<UserResponseModel> Instructors { get; private set; } = new List<UserResponseModel>();
        public IEnumerable<UserResponseModel> Students { get; private set; } = new List<UserResponseModel>();

        public CreateModel(IMapper mapper, IEnrollmentService enrollmentService, IUserService userService)
        {
            _mapper = mapper;
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
            if(!ModelState.IsValid) return Page();

            var enrollment = _mapper.Map<Enrollment>(Input);
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
