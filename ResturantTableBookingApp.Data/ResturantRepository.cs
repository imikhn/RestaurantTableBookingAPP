using Microsoft.EntityFrameworkCore;
using ResturantTableBookingApp.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ResturantTableBookingApp.Data
{
    public class ResturantRepository : IResturantRepository
    {
        private readonly ResturantTableBookingDbContext _context;
        public ResturantRepository(ResturantTableBookingDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Async/await keyword should be used where you need to manipulate the record,until that time return task is 
        /// </summary>
        /// <returns></returns>
        public Task<List<ResturantModel>> GetAllResturantsAsync()
        {
            var resturantsList = _context.Restaurants
                .OrderBy(x => x.Name)
                .Select(x => new ResturantModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    Phone = x.Phone,
                    Email = x.Email,
                    ImageUrl = x.ImageUrl
                }).ToListAsync();

            return resturantsList;
        }

        public async Task<IEnumerable<DinningTableWithTimeSlotModel>> GetDiningTablesByBranchAsync(int branchId, DateTime date)
        {
            // LINQ Lambda Expression
            var query = _context.DiningTables // This start the query by selecting all entities from the DiningTables Table.
                .Where(dt => dt.RestaurantBranchId == branchId) // This filters the DiningTable Entites to only those where the resturantbranchid match with the branchid 
                .SelectMany(dt => dt.TimeSlots, (dt, ts) => new // this perform flattening operation using the selectmany method. Its assosiates each diningtable with its corresponding TImeSlots and create new annonymous object with the properties from both the DiningTable (dt) and TimeSlot(ts). THe new object will contain property of both the table
                {
                    dt.RestaurantBranchId,
                    dt.TableName,
                    dt.Capacity,
                    ts.ReservationDay,
                    ts.MealType,
                    ts.TableStatus,
                    ts.Id
                })
                .Where(ts => ts.ReservationDay.Date == date.Date)
                .OrderBy(ts => ts.Id)
                .ThenBy(ts => ts.MealType);
            //.ToListAsync();

            // Print the generated SQL query
            var sqlQuery = query.ToQueryString();
            Console.WriteLine("Generated SQL Query: \n" + sqlQuery);

            // Execute the query and get the results
            var diningTables = await query.ToListAsync();

            return diningTables.Select(dt => new DinningTableWithTimeSlotModel()
            {
                MealType = dt.MealType,
                TableStatus = dt.TableStatus,
                BranchId = branchId,
                Capacity = dt.Capacity,
                ReservationDay = dt.ReservationDay,
                TableName = dt.TableName,
                TimeSlotId = dt.Id
            });
        }

        public async Task<IEnumerable<DinningTableWithTimeSlotModel>> GetDiningTablesByBranchAsync(int branchId)
        {
            var data = await (
                from rb in _context.RestaurantBranches
                join dt in _context.DiningTables on rb.Id equals dt.RestaurantBranchId
                join ts in _context.TimeSlots on dt.Id equals ts.DiningTableId
                where dt.RestaurantBranchId == branchId //&& ts.ReservationDay >= DateTime.Now.Date
                orderby ts.Id, ts.MealType
                select new DinningTableWithTimeSlotModel()
                {
                    BranchId = rb.Id,
                    Capacity = dt.Capacity,
                    TableName = dt.TableName,
                    TimeSlotId = ts.Id,
                    MealType = ts.MealType,
                    ReservationDay = ts.ReservationDay,
                    TableStatus = ts.TableStatus
                })
                .ToListAsync();
            return data;
        }

        public async Task<IEnumerable<ResturantBranchModel>> GetResturantBranchsByResturantIdAsync(int resturantId)
        {
            var resturantBranch = await _context.RestaurantBranches
                .Where(x => x.RestaurantId == resturantId)
                .Select(x => new ResturantBranchModel()
                {
                    Id = x.Id,
                    Address = x.Address,
                    Phone = x.Phone,
                    Email = x.Email,
                    ImageUrl = x.ImageUrl,
                    Name = x.Name
                })
                .ToListAsync();

            return resturantBranch;
        }
    }
}
