#nullable disable

using AutoMapper;
using DSMS.Application.Models.Feedback;
using DSMS.Application.Services;
using DSMS.Core.Entities.Identity;
using DSMS.DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace DSMS.Frontend.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private readonly Mapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFeedbackService _feedbackService;

        public DetailsModel(IUserRepository userRepository,
            UserManager<ApplicationUser> userManager,
            IFeedbackService feedbackService,
            Mapper mapper)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _feedbackService = feedbackService;
            _mapper = mapper;
        }

        [BindProperty]
        public CreateFeedbackModel Input { get; set; }

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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            
            var feedback = _mapper.Map<CreateFeedbackModel>(Input);
            var student = _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            feedback.InstructorId = ApplicationUser.Id;


            try
            {
                await _feedbackService.CreateAsync(feedback);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Redirect("~/Users/Index");
        }
    }
}
