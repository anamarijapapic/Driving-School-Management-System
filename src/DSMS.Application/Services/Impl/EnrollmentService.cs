using AutoMapper;
using DSMS.Application.Models.Enrollments;
using DSMS.Core.Entities;
using DSMS.Core.Entities.Identity;
using DSMS.DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;

namespace DSMS.Application.Services.Impl
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IMapper _mapper;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public EnrollmentService(IMapper mapper, IEnrollmentRepository enrollmentRepository, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _enrollmentRepository = enrollmentRepository;
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
            var enrollments = await _enrollmentRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<EnrollmentResponseModel>>(enrollments);
        }

        public IEnumerable<EnrollmentResponseModel> Search(IEnumerable<EnrollmentResponseModel> enrollments, string searchString)
        {
            IEnumerable<EnrollmentResponseModel> searchedEnrollments = enrollments;

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

        public IEnumerable<EnrollmentResponseModel> Filter(IEnumerable<EnrollmentResponseModel> enrollments, string currentFilter)
        {
            IEnumerable<EnrollmentResponseModel> filteredEnrollments = enrollments;

            if (!string.IsNullOrEmpty(currentFilter))
            {
                var currentFilterTrim = currentFilter.Trim();
                filteredEnrollments = enrollments.Where(e => e.Category.ToString() == currentFilterTrim);
            }

            return filteredEnrollments;
        }
    }
}
