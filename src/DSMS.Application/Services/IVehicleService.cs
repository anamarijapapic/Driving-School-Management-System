using DSMS.Application.Models.Vehicle;
using DSMS.Core.Entities;

namespace DSMS.Application.Services
{
    public interface IVehicleService
    {
        Task<CreateVehicleResponseModel> CreateAsync(CreateVehicleModel createVehicleModel);

        Task<IEnumerable<VehicleResponseModel>> GetAllAsync();

        Task<Vehicle> GetByIdAsync(string id);

        Task<Vehicle> UpdateAsync(Vehicle vehicle);

        Task<Vehicle> DeleteAsync(Vehicle vehicle);

        IEnumerable<VehicleResponseModel> Search(IEnumerable<VehicleResponseModel> vehicles, string searchString);

        IEnumerable<VehicleResponseModel> Filter(IEnumerable<VehicleResponseModel> vehicles, string currentFilter);
    }
}
