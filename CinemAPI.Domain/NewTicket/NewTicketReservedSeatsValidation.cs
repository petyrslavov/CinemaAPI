using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Reservation;
using CinemAPI.Models.Contracts.Ticket;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewTicket
{
    public class NewTicketReservedSeatsValidation : INewTicket
    {
        private readonly IReservationRepository reserveRepo;
        private readonly INewTicket newTicket;

        public NewTicketReservedSeatsValidation(IReservationRepository reserveRepo, INewTicket newTicket)
        {
            this.reserveRepo = reserveRepo;
            this.newTicket = newTicket;
        }

        public async Task<NewCreationSummary> New(ITicketCreation ticket)
        {
            IReservation reservationDb = await reserveRepo.Get(ticket.Row, ticket.Column, ticket.ProjectionId);

            if (reservationDb != null)
            {
                return new NewCreationSummary(false, "Cannot buy reserved seats");
            }

            return await newTicket.New(ticket);
        }
    }
}
