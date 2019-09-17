using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.ReservationTicket;


namespace CinemAPI.Domain.Contracts
{
    public interface INewReservationTicket
    {
        NewReservationTicketSummary New(IReservationTicketCreation reservationTicket);
    }
}
