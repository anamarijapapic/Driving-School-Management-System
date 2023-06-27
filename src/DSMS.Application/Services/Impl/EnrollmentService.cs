using AutoMapper;
using DSMS.Application.Models.Enrollments;
using DSMS.Core.Entities;
using DSMS.Core.Entities.Identity;
using DSMS.Core.Enums;
using DSMS.DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DSMS.Application.Services.Impl
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IMapper _mapper;

        private readonly IEnrollmentRepository _enrollmentRepository;

        private readonly IAppointmentRepository _appointmentRepository;

        private readonly UserManager<ApplicationUser> _userManager;

        public EnrollmentService(IMapper mapper,
            IEnrollmentRepository enrollmentRepository,
            IAppointmentRepository appointmentRepository,
            UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _enrollmentRepository = enrollmentRepository;
            _appointmentRepository = appointmentRepository;
            _userManager = userManager;
        }

        public async Task<CreateEnrollmentResponseModel> CreateAsync(CreateEnrollmentModel createEnrollmentModel)
        {
            var instructor = await _userManager.FindByIdAsync(createEnrollmentModel.InstructorId);
            var student = await _userManager.FindByIdAsync(createEnrollmentModel?.StudentId);
            var enrollment = _mapper.Map<Enrollment>(createEnrollmentModel);

            enrollment.Student = student;
            enrollment.Instructor = instructor;

            return new CreateEnrollmentResponseModel
            {
                Id = (await _enrollmentRepository.AddAsync(enrollment)).Id,
            };
        }

        public async Task<IEnumerable<EnrollmentResponseModel>> GetAllAsync()
        {
            var enrollments = await _enrollmentRepository.GetAll().ToListAsync();

            return _mapper.Map<IEnumerable<EnrollmentResponseModel>>(enrollments);
        }

        public async Task<IEnumerable<EnrollmentResponseModel>> GetByStudentAsync(ApplicationUser student)
        {
            var enrollments = await _enrollmentRepository.GetByStudent(student).ToListAsync();

            return _mapper.Map<IEnumerable<EnrollmentResponseModel>>(enrollments);
        }

        public async Task<Enrollment> GetByIdAsync(string id)
        {
            var enrollment = (await _enrollmentRepository.GetAllAsync(a => a.Id.ToString() == id)).FirstOrDefault();

            return enrollment;
        }

        public async Task<Enrollment> UpdateAsync(Enrollment enrollment)
        {
            return await _enrollmentRepository.UpdateAsync(enrollment);
        }

        public async Task<Enrollment> DeleteAsync(Enrollment enrollment)
        {
            var fullEnrollment = _enrollmentRepository.GetById(enrollment.Id.ToString());
            var studentAppointments = await _appointmentRepository.GetByStudent(fullEnrollment.Student).ToListAsync();

            foreach (var appointment in studentAppointments)
            {
                appointment.Status = AppointmentStatus.Canceled;
                await _appointmentRepository.UpdateAsync(appointment);
            }

            return await _enrollmentRepository.DeleteAsync(enrollment);
        }

        public IEnumerable<EnrollmentResponseModel> Search(IEnumerable<EnrollmentResponseModel> enrollments,
            string searchString)
        {
            var searchedEnrollments = enrollments;

            if (!string.IsNullOrEmpty(searchString))
            {
                var searchStringTrim = searchString.ToUpper().Trim();
                searchedEnrollments = enrollments
                    .Where(e => e.Instructor.FirstName.Contains(searchStringTrim) ||
                    e.Instructor.LastName.ToUpper().Contains(searchStringTrim) ||
                    e.Student.FirstName.ToUpper().Contains(searchStringTrim) ||
                    e.Student.LastName.ToUpper().Contains(searchStringTrim));
            }

            return searchedEnrollments;
        }

        public IEnumerable<EnrollmentResponseModel> Filter(IEnumerable<EnrollmentResponseModel> enrollments,
            string currentFilter)
        {
            var filteredEnrollments = enrollments;

            if (!string.IsNullOrEmpty(currentFilter))
            {
                var currentFilterTrim = currentFilter.Trim();
                filteredEnrollments = enrollments.Where(e => e.Category.ToString() == currentFilterTrim);
            }

            return filteredEnrollments;
        }
    }
}
