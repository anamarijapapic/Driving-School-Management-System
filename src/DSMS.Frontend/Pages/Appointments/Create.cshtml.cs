#nullable disable

using DSMS.Application.Models.Appointment;
using DSMS.Application.Services;
using DSMS.Core.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DSMS.Frontend.Pages.Appointments
{
    [Authorize(Roles = ("Student"))]
    public class CreateModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;

        private readonly IEnrollmentService _enrollmentService;

        private readonly UserManager<ApplicationUser> _userManager;

        public string StudentId { get; set; }

        public string InstructorId { get; set; }

        public List<DateOnly> Dates = new();

        public List<TimeOnly> Slots = new();

        public IDictionary<DateOnly, IEnumerable<TimeOnly>> Schedule = new Dictionary<DateOnly, IEnumerable<TimeOnly>>();

        public CreateModel(IAppointmentService appointmentService,
            IEnrollmentService enrollmentService,
            UserManager<ApplicationUser> userManager)
        {
            _appointmentService = appointmentService;
            _enrollmentService = enrollmentService;
            _userManager = userManager;
        }

        [BindProperty]
        public CreateAppointmentModel Input { get; set; }

        public void InitializeDateTime()
        {
            var date = DateOnly.FromDateTime(DateTime.Now);
            var daysAdded = 0;

            while (daysAdded < 5)
            {
                date = date.AddDays(1);
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    Dates.Add(date);
                    daysAdded++;
                }
            }

            var time = new TimeOnly(9, 00, 00);
            var hoursAdded = 0;

            while (hoursAdded < 8)
            {
                time = time.AddHours(1);
                Slots.Add(time);
                hoursAdded++;
            }
        }

        public async Task InitializeSchedule(ApplicationUser instructor)
        {
            foreach (var date in Dates)
            {
                var reservedSlots = await _appointmentService.GetReservedSlotsByInstructorAndDateAsync(instructor, date);

                var freeSlots = Slots.ConvertAll(sl => sl);
                freeSlots.RemoveAll(sl => reservedSlots.Contains(sl));

                Schedule.Add(date, freeSlots);
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var student = await _userManager.GetUserAsync(User);

            var instructor = (await _enrollmentService.GetByStudentAsync(student)).FirstOrDefault()?.Instructor;
            if (instructor == null)
            {
                return base.NotFound($"Unable to load instructor for user with ID '{student.Id}'.");
            }

            StudentId = student.Id;
            InstructorId = instructor.Id;

            InitializeDateTime();

            await InitializeSchedule(instructor);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string studentId, string instructorId, string date, string slot)
        {
            Input.Student = await _userManager.FindByIdAsync(studentId);
            Input.Instructor = await _userManager.FindByIdAsync(instructorId);
            Input.Date = DateOnly.Parse(date);
            Input.Start = TimeOnly.Parse(slot);
            Input.End = Input.Start.AddHours(1);
            Input.Status = Core.Enums.AppointmentStatus.Reserved;

            try
            {
                await _appointmentService.CreateAsync(Input);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Redirect("~/Appointments/Index");
        }
    }
}
