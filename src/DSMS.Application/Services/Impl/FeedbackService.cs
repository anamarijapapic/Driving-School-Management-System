﻿using AutoMapper;
using DSMS.Application.Models.Feedback;
using DSMS.Core.Entities;
using DSMS.Core.Entities.Identity;
using DSMS.DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DSMS.Application.Services.Impl
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IMapper _mapper;

        private readonly IFeedbackRepository _feedbackRepository;

        private readonly UserManager<ApplicationUser> _userManager;

        public FeedbackService(IMapper mapper,
            IFeedbackRepository feedbackRepository,
            UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _feedbackRepository = feedbackRepository;
            _userManager = userManager;
        }

        public async Task<CreateFeedbackResponseModel> CreateAsync(CreateFeedbackModel createFeedbackModel)
        {
            var instructor = await _userManager.FindByIdAsync(createFeedbackModel.InstructorId);
            var student = await _userManager.FindByIdAsync(createFeedbackModel.StudentId);
            var feedback = _mapper.Map<Feedback>(createFeedbackModel);

            feedback.Student = student;
            feedback.Instructor = instructor;

            return new CreateFeedbackResponseModel
            {
                Id = (await _feedbackRepository.AddAsync(feedback)).Id,
            };
        }

        public async Task<IEnumerable<Feedback>> GetAllAsync()
        {
            var feedbacks = await _feedbackRepository.GetAll().ToListAsync();

            return _mapper.Map<IEnumerable<Feedback>>(feedbacks);
        }

        public async Task<IEnumerable<Feedback>> GetByInstructorAsync(string id)
        {
            var feedbacks = await _feedbackRepository.GetAllAsync(f => f.Instructor.Id == id);

            return _mapper.Map<IEnumerable<Feedback>>(feedbacks);
        }

        public async Task<Feedback> GetByIdAsync(string id)
        {
            var feedback = (await _feedbackRepository.GetAllAsync(f => f.Id.ToString() == id)).FirstOrDefault();

            return feedback;
        }

        public async Task<Feedback> UpdateAsync(Feedback feedback)
        {
            return await _feedbackRepository.UpdateAsync(feedback);
        }

        public async Task<Feedback> DeleteAsync(Feedback feedback)
        {
            return await _feedbackRepository.DeleteAsync(feedback);
        }
    }
}
