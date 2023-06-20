#nullable disable

using Microsoft.AspNetCore.Mvc.RazorPages;
using DSMS.Application.Models;
using DSMS.Application.Models.Enrollments;
using DSMS.Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace DSMS.Frontend.Pages.Enrollments
{
    [Authorize(Roles = ("Administrator"))]
    public class IndexModel : PageModel
    {
        private readonly IEnrollmentService _enrollmentService;

        public PaginatedList<EnrollmentResponseModel> Enrollments { get; set; }

        public IndexModel(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        public async Task<PageResult> OnGetAsync(string searchString, string currentFilter, int? pageIndex)
        {
            if (searchString!= null)
            {
                pageIndex = 1;
            }
            var pageSize = 5;
            var enrollments = await _enrollmentService.GetAllAsync();

            ViewData["Keyword"] = searchString;
            enrollments = _enrollmentService.Search(enrollments, searchString);

            ViewData["CurrentFilter"] = currentFilter;
            enrollments = _enrollmentService.Filter(enrollments, currentFilter);

            Enrollments = PaginatedList<EnrollmentResponseModel>.Create(enrollments, pageIndex ?? 1, pageSize);

            return Page();
        }
    }
}
