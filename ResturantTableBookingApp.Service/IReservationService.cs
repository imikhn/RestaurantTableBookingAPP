using ResturantTableBookingApp.Core;
using ResturantTableBookingApp.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantTableBookingApp.Service
{
    public interface IReservationService
    {
        public Task<DinningTableWithTimeSlotModel> CheckInReservationAsync(DinningTableWithTimeSlotModel reservation);

        public Task<int> CreateOrUpdateReservationAsync(ReservationModel reservation);

        public Task<bool> TimeSlotExistAsync(int timeslotId);
    }
}
