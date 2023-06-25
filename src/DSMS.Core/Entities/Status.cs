using DSMS.Core.Enums;

namespace DSMS.Core.Entities
{
    public class Status
    {
        public AppointmentStatus Id { get; set; }

        public string Name { get; set; }

        public ICollection<Appointment> Appointments { get; } = new List<Appointment>();
    }
}
