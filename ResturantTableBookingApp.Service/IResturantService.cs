using ResturantTableBookingApp.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantTableBookingApp.Service
{
    public interface IResturantService
    {
        Task<List<ResturantModel>> GetAllResturantsAsync();
        Task<IEnumerable<ResturantBranchModel>> GetResturantBranchsByResturantIdAsync(int resturantId);
        Task<IEnumerable<DinningTableWithTimeSlotModel>> GetDiningTablesByBranchAsync(int branchId, DateTime date);
        Task<IEnumerable<DinningTableWithTimeSlotModel>> GetDiningTablesByBranchAsync(int branchId);
    }
}
