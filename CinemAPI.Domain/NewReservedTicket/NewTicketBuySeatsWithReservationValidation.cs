using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Ticket;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewReservedTicket
{
    public class NewTicketBuySeatsWithReservationValidation: INewReservedTicket
    {
        private readonly IReservationRepository reserveRepo;
        private readonly INewReservedTicket newTicket;

        public NewTicketBuySeatsWithReservationValidation(IReservationRepository reserveRepo, INewReservedTicket newTicket)
        {
            this.reserveRepo = reserveRepo;
            this.newTicket = newTicket;
        }

        public async Task<NewReservedTicketSummary> New(ITicketCreation ticket)
        {
            var reservations = await reserveRepo.GetAllReservations();
            await reserveRepo.CancelExpiredReservations(reservations);

            var reservationDb = await reserveRepo.Get(ticket.Row, ticket.Column, ticket.ProjectionId);

            if (reservationDb == null)
            {
                return new NewReservedTicketSummary(false, "Cannot buy seats for expired reservation");
            }

            return await newTicket.New(ticket);
        }
    }
}
