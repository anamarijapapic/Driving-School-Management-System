using DSMS.Application.Models.Vehicle;

namespace DSMS.Application.Services
{
    public interface IVehicleService
    {
        Task<CreateVehicleResponseModel> CreateAsync(CreateVehicleModel createVehicleModel);

        Task<IEnumerable<VehicleResponseModel>> GetAllAsync();

        IEnumerable<VehicleResponseModel> Search(IEnumerable<VehicleResponseModel> vehicles, string searchString);

        IEnumerable<VehicleResponseModel> Filter(IEnumerable<VehicleResponseModel> vehicles, string currentFilter);
    }
}
