using Microsoft.AspNetCore.Identity;

namespace DSMS.DataAccess.Identity;

public class ApplicationUser : IdentityUser 
{ 
    private string FirstName { get; set; }

    private string LastName { get; set; }

    private DateTime DateOfBirth { get; set; }

    private byte[] ProfilePicture { get; set; }

    private string Oib { get; set; }

    private string IdCardNumber { get; set; }
}
