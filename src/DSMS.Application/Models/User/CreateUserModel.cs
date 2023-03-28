namespace DSMS.Application.Models.User;

public class CreateUserModel
{
    public Guid Id { get; set; }

    public Guid RoleId { get; set; }

    public string Username { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    private string FirstName { get; set; }

    private string LastName { get; set; }

    private DateTime DateOfBirth { get; set; }

    private byte[] ProfilePicture { get; set; }

    private string Oib { get; set; }

    private string IdCardNumber { get; set; }
}

public class CreateUserResponseModel : BaseResponseModel { }
