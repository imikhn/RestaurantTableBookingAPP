using ResturantTableBookingApp.Core.ViewModel;

namespace ResturantTableBookingApp.Data
{
    public interface IResturantRepository
    {
        Task<List<ResturantModel>> GetAllResturantsAsync();

        Task<IEnumerable<ResturantBranchModel>> GetResturantBranchsByResturantIdAsync(int resturantId);

        /// <summary>
        /// LINQ query reterives dining tables and their assosiated time slot for a specific branchid
        /// and date.The result is sorted by Id and then meal type. the data is then projected into a list of
        /// DiningTableWithTimeSlotModel viewmodel and returned
        /// </summary>
        /// <param name="branchId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        Task<IEnumerable<DinningTableWithTimeSlotModel>> GetDiningTablesByBranchAsync(int branchId, DateTime date);

        /// <summary>
        /// LINQ query reterives dining tables and their assosiated time slot for a specific branchId and date which are
        /// current and future . The result is sorted by id and then meal type. The data is then projected into a list of 
        /// DinningTableWithTimeSlotModel viewmodel and returned.
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        Task<IEnumerable<DinningTableWithTimeSlotModel>> GetDiningTablesByBranchAsync(int branchId);
    }
}
