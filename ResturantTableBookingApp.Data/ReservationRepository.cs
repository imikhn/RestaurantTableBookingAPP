using Microsoft.EntityFrameworkCore;
using ResturantTableBookingApp.Core;
using ResturantTableBookingApp.Core.ViewModel;

namespace ResturantTableBookingApp.Data
{
    public class ReservationRepository : IReservationRepository
    {
        public readonly ResturantTableBookingDbContext _dbContext;

        public ReservationRepository(ResturantTableBookingDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<int> CreateOrUpdateReservationAsync(ReservationModel reservationModel)
        {
            throw new NotImplementedException();
        }

        public Task<TimeSlot> GetTimeSlotByIdAsync(int timeSlotId)
        {
            throw new NotImplementedException();
        }

        public async Task<DinningTableWithTimeSlotModel> UpdateReservationAsync(DinningTableWithTimeSlotModel reservation)
        {
            var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == reservation.UserEmailId);

            var timeslotToUpdate = await _dbContext.TimeSlots.FindAsync(reservation.TimeSlotId);
            timeslotToUpdate.TableStatus = "Checked In";
            _dbContext.TimeSlots.Update(timeslotToUpdate);

            var reservationToUpdate = await _dbContext.Reservations.FirstAsync(f => f.TimeSlotId == reservation.TimeSlotId);
            reservationToUpdate.ReservationStatus = "Checked In";
            _dbContext.Reservations.Update(reservationToUpdate);

            await _dbContext.SaveChangesAsync();

            return reservation;


        }
    }
}
