using DSMS.Application.Models.Appointment;
using DSMS.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMS.Application.Services
{
    public interface IAppointmentService
    {
        Task<AppointmentResponseModel> CreateAsync(AppointmentModel model);

        Task<IEnumerable<AppointmentResponseModel>> GetAllAsync();

        Task<IEnumerable<AppointmentResponseModel>> GetByInstructorAsync(ApplicationUser instructor);
    }
}
