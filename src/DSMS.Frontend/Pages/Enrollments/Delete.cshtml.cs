#nullable disable

using DSMS.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSMS.Frontend.Pages.Enrollments
{
    public class DeleteModel : PageModel
    {
        private readonly IEnrollmentService _enrollmentService;

        public DeleteModel(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var enrollment = await _enrollmentService.GetByIdAsync(id);
            if (enrollment == null)
            {
                return base.NotFound($"Unable to find enrollment with ID '{id}'.");
            }

            return Page();

        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            var enrollment = await _enrollmentService.GetByIdAsync(id);
            if (enrollment == null)
            {
                return base.NotFound($"Unable to find enrollment with ID '{id}'.");
            }

            await _enrollmentService.DeleteAsync(enrollment);

            return Redirect("~/Enrollments/Index");
        }
    }
}
