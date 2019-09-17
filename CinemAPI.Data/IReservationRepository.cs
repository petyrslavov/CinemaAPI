using CinemAPI.Models.Contracts.Reservation;
using System.Collections.Generic;

namespace CinemAPI.Data
{
    public interface IReservationRepository
    {
        IReservation Insert(IReservationCreation reservation);

        IReservation Get(int row, int column, long projectionId);

        IEnumerable<IReservation> GetAllReservations();

        void CancelExpiredReservations(IEnumerable<IReservation> reservations);
    }
}
