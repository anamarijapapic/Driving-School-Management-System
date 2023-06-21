using AutoMapper;
using DSMS.Application.Models.Vehicle;
using DSMS.Core.Entities;

namespace DSMS.Application.MappingProfiles;

public class VehicleProfile : Profile
{
    public VehicleProfile()
    {
        CreateMap<CreateVehicleModel, Vehicle>();
        CreateMap<Vehicle, VehicleResponseModel>();
    }
}
