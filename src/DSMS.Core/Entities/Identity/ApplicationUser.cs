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

    public ICollection<Appointment> Appointments { get; } = new List<Appointment>();

    public List<Category> Categories { get; } = new();

    public ICollection<Enrollment> StudentEnrollments { get; } = new List<Enrollment>();

    public ICollection<Enrollment> InstructorEnrollments { get; } = new List<Enrollment>();

    public ICollection<Feedback> StudentFeedbacks { get; } = new List<Feedback>();

    public ICollection<Feedback> InstructorFeedbacks { get; } = new List<Feedback>();

    public ICollection<Reaction> Reactions { get; } = new List<Reaction>();

    public ICollection<ScheduleSlot> ScheduleSlots { get; } = new List<ScheduleSlot>();

    public ICollection<Vehicle> Vehicles { get; } = new List<Vehicle>();
}
