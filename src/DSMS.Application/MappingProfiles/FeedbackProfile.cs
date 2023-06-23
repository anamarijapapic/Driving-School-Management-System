using AutoMapper;
using DSMS.Application.Models.Feedback;
using DSMS.Core.Entities;

namespace DSMS.Application.MappingProfiles
{
    public class FeedbackProfile : Profile
    {
            public FeedbackProfile()
            {
            CreateMap<CreateFeedbackModel, Feedback>();
            CreateMap<Feedback, FeedbackResponseModel>();

            CreateMap<UpdateFeedbackModel, Feedback>();
        }
        }
    }

