using ResturantTableBookingApp.Core;
using ResturantTableBookingApp.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantTableBookingApp.Data
{
    public interface IReservationRepository
    {
        Task<int> CreateOrUpdateReservationAsync(ReservationModel reservationModel);

        Task<TimeSlot> GetTimeSlotByIdAsync(int timeSlotId);

        Task<DinningTableWithTimeSlotModel> UpdateReservationAsync(DinningTableWithTimeSlotModel dinningTableWithTimeSlotModel);
    }
}
