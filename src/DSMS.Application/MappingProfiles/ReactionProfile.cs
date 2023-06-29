using AutoMapper;
using DSMS.Application.Models.Reaction;
using DSMS.Core.Entities;

namespace DSMS.Application.MappingProfiles;

public class ReactionProfile : Profile
{
    public ReactionProfile()
    {
        CreateMap<CreateReactionModel, Reaction>();
        CreateMap<Reaction, ReactionResponseModel>();
    }
}
