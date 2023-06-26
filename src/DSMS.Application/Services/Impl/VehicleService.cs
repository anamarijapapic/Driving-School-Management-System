using AutoMapper;
using DSMS.Application.Models.Vehicle;
using DSMS.Core.Entities;
using DSMS.Core.Entities.Identity;
using DSMS.DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;

namespace DSMS.Application.Services.Impl
{
    public class VehicleService : IVehicleService
    {
        private readonly IMapper _mapper;

        private readonly IVehicleRepository _vehicleRepository;

        private readonly UserManager<ApplicationUser> _userManager;

        public VehicleService(IMapper mapper,
            IVehicleRepository vehicleRepository,
            UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _vehicleRepository = vehicleRepository;
            _userManager = userManager;
        }

        public async Task<CreateVehicleResponseModel> CreateAsync(CreateVehicleModel createVehicleModel)
        {
            var instructor = await _userManager.FindByIdAsync(createVehicleModel.InstructorId);
            var vehicle = _mapper.Map<Vehicle>(createVehicleModel);

            vehicle.Instructor = instructor;

            return new CreateVehicleResponseModel
            {
                Id = (await _vehicleRepository.AddAsync(vehicle)).Id
            };
        }

        public async Task<IEnumerable<VehicleResponseModel>> GetAllAsync()
        {
            var vehicles = await _vehicleRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<VehicleResponseModel>>(vehicles);
        }

        public async Task<Vehicle> GetByIdAsync(string id)
        {
            return (await _vehicleRepository.GetAllAsync(a => a.Id.ToString() == id)).FirstOrDefault();
        }

        public async Task<Vehicle> UpdateAsync(Vehicle vehicle)
        {
            return await _vehicleRepository.UpdateAsync(vehicle);
        }

        public async Task<Vehicle> DeleteAsync(Vehicle vehicle)
        {
            return await _vehicleRepository.DeleteAsync(vehicle);
        }

        public IEnumerable<VehicleResponseModel> Search(IEnumerable<VehicleResponseModel> vehicles, string searchString)
        {
            IEnumerable<VehicleResponseModel> searchedVehicles = vehicles;

            if (!string.IsNullOrEmpty(searchString))
            {
                var searchStringTrim = searchString.ToLower().Trim();
                searchedVehicles = vehicles.Where(v => v.Description.ToLower().Contains(searchStringTrim));
            }

            return searchedVehicles;
        }

        public IEnumerable<VehicleResponseModel> Filter(IEnumerable<VehicleResponseModel> vehicles, string currentFilter)
        {
            IEnumerable<VehicleResponseModel> filteredVehicles = vehicles;

            if (!string.IsNullOrEmpty(currentFilter))
            {
                var currentFilterTrim = currentFilter.Trim();
                filteredVehicles = vehicles.Where(v => v.Category.ToString() == currentFilter);
            }

            return filteredVehicles;
        }
    }
}
