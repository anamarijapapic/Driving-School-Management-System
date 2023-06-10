#nullable disable

using DSMS.Core.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace DSMS.Frontend.Pages.Users
{
    [Authorize(Roles = ("Administrator"))]
    public class EditModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public EditModel(
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]
            [DataType(DataType.Date)]
            [Display(Name = "Date Of Birth")]
            public DateTime DateOfBirth { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [StringLength(11, ErrorMessage = "The {0} must be {1} characters long.", MinimumLength = 11)]
            [Display(Name = "OIB")]
            public string Oib { get; set; }

            [Required]
            [StringLength(9, ErrorMessage = "The {0} must be {1} characters long.", MinimumLength = 9)]
            [DataType(DataType.Text)]
            [Display(Name = "ID Card Number")]
            public string IdCardNumber { get; set; }

            [Display(Name = "Profile Photo")]
            public byte[] Photo { get; set; }

            [Required]
            [Display(Name = "Role")]
            public string Role { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var firstName = user.FirstName;
            var lastName = user.LastName;
            var dateOfBirth = user.DateOfBirth;
            var oib = user.Oib;
            var idCardNumber = user.IdCardNumber;
            var photo = user.Photo;

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.First().ToString();

            Input = new InputModel
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                Oib = oib,
                IdCardNumber = idCardNumber,
                Photo = photo,
                Role = role
            };
        }

        public async Task<IActionResult> OnGetAsync(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return base.NotFound($"Unable to load user with ID '{Id}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return base.NotFound($"Unable to load user with ID '{Id}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var firstName = user.FirstName;
            if (Input.FirstName != firstName)
            {
                user.FirstName = Input.FirstName;
                await _userManager.UpdateAsync(user);
            }

            var lastName = user.LastName;
            if (Input.LastName != lastName)
            {
                user.LastName = Input.LastName;
                await _userManager.UpdateAsync(user);
            }

            var dateOfBirth = user.DateOfBirth;
            if (Input.DateOfBirth != dateOfBirth)
            {
                user.DateOfBirth = Input.DateOfBirth;
                await _userManager.UpdateAsync(user);
            }

            var oib = user.Oib;
            if (Input.Oib != oib)
            {
                user.Oib = Input.Oib;
                await _userManager.UpdateAsync(user);
            }

            var idCardNumber = user.IdCardNumber;
            if (Input.IdCardNumber != idCardNumber)
            {
                user.IdCardNumber = Input.IdCardNumber;
                await _userManager.UpdateAsync(user);
            }

            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files.FirstOrDefault();
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    user.Photo = dataStream.ToArray();
                }
                await _userManager.UpdateAsync(user);
            }

            var role = _userManager.GetRolesAsync(user).Result.First();
            if (Input.Role != role)
            {
                await _userManager.RemoveFromRoleAsync(user, role);
                await _userManager.AddToRoleAsync(user, Input.Role);
            }

            StatusMessage = "User details have been updated";
            return Page();
        }
    }
}
