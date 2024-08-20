using ResturantTableBookingApp.Core.ViewModel;
using ResturantTableBookingApp.Data;

namespace ResturantTableBookingApp.Service
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }
        public Task<DinningTableWithTimeSlotModel> CheckInReservationAsync(DinningTableWithTimeSlotModel reservation)
        {
            return _reservationRepository.UpdateReservationAsync(reservation);
        }

        public Task<int> CreateOrUpdateReservationAsync(ReservationModel reservation)
        {
            return _reservationRepository.CreateOrUpdateReservationAsync(reservation);
        }

        public async Task<bool> TimeSlotExistAsync(int timeslotId)
        {
            var timeSlot = await _reservationRepository.GetTimeSlotByIdAsync(timeslotId);

            return timeSlot != null;
        }
    }
}
