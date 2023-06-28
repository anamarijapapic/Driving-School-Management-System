using Microsoft.AspNetCore.Identity;

namespace DSMS.Core.Entities.Identity;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime DateOfBirth { get; set; }

    public byte[] Photo { get; set; }

    public string Oib { get; set; }

    public string IdCardNumber { get; set; }

    public ICollection<Enrollment> StudentEnrollments { get; } = new List<Enrollment>();

    public ICollection<Enrollment> InstructorEnrollments { get; } = new List<Enrollment>();

    public ICollection<Feedback> StudentFeedbacks { get; } = new List<Feedback>();

    public ICollection<Feedback> InstructorFeedbacks { get; } = new List<Feedback>();

    public ICollection<Appointment> StudentAppointments { get; } = new List<Appointment>();

    public ICollection<Appointment> InstructorAppointments { get; } = new List<Appointment>();

    public ICollection<Reaction> Reactions { get; } = new List<Reaction>();

    public ICollection<Vehicle> Vehicles { get; } = new List<Vehicle>();
}
