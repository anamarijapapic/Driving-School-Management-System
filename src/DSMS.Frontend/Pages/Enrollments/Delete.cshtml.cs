#nullable disable

using DSMS.Core.Entities;
using DSMS.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSMS.Frontend.Pages.Enrollments
{
    public class DeleteModel : PageModel
    {
        private readonly IEnrollmentRepository _enrollmentRepository;

        public DeleteModel(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        public async Task<IActionResult> OnGetAsync(string Id)
        {
            var repoEnrollment = await _enrollmentRepository.GetAllAsync(e => e.Id.ToString() == Id);
            var enrollment = repoEnrollment.First();
            if (enrollment == null)
            {
                return base.NotFound($"Unable to find enrollment with ID '{Id}'.");
            }


            return Page();

        }

        public async Task<IActionResult> OnPostAsync(string Id)
        {
            var repoEnrollment = await _enrollmentRepository.GetAllAsync(e => e.Id.ToString() == Id);
            var enrollment = repoEnrollment.First();
            if (enrollment == null)
            {
                return base.NotFound($"Unable to find enrollment with ID '{Id}'.");
            }

            await _enrollmentRepository.DeleteAsync(enrollment);

            return Redirect("~/Enrollments/Index");

        }
    }
}
