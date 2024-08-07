using ResturantTableBookingApp.Core.ViewModel;
using ResturantTableBookingApp.Data;

namespace ResturantTableBookingApp.Service
{
    public class ResturantService : IResturantService
    {
        private  readonly IResturantRepository _resturantRepository;
        public ResturantService(IResturantRepository repository)
        {
            _resturantRepository = repository;
        }
        public Task<List<ResturantModel>> GetAllResturantsAsync()
        {
            return _resturantRepository.GetAllResturantsAsync();
        }

        public Task<IEnumerable<DinningTableWithTimeSlotModel>> GetDiningTablesByBranchAsync(int branchId, DateTime date)
        {
            return _resturantRepository.GetDiningTablesByBranchAsync(branchId, date);
        }

        public Task<IEnumerable<DinningTableWithTimeSlotModel>> GetDiningTablesByBranchAsync(int branchId)
        {
            return _resturantRepository.GetDiningTablesByBranchAsync(branchId);
        }

        public Task<IEnumerable<ResturantBranchModel>> GetResturantBranchsByResturantIdAsync(int resturantId)
        {
            return _resturantRepository.GetResturantBranchsByResturantIdAsync(resturantId);
        }
    }
}
