using DSMS.Core.Enums;

namespace DSMS.Application.Models.User
{
    public class UserIndexModel : BaseResponseModel
    {
        public ApplicationRole Role { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public byte[] ProfilePicture { get; set; }

        public string Oib { get; set; }

        public string IdCardNumber { get; set; }
    }
}
