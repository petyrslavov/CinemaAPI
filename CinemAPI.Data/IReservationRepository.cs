using CinemAPI.Models.Contracts.Reservation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemAPI.Data
{
    public interface IReservationRepository
    {
        Task<IReservation> Insert(IReservationCreation reservation);

        Task<IReservation> Get(int row, int column, long projectionId);

        IReservation GetById(int reservationId);

        Task<IEnumerable<IReservation>> GetAllReservations();

        Task CancelExpiredReservations(IEnumerable<IReservation> reservations);
    }
}
