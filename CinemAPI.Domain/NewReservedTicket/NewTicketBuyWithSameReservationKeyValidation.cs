using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Ticket;
using System.Linq;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewReservedTicket
{
    public class NewTicketBuyWithSameReservationKeyValidation: INewReservedTicket
    {
        private readonly ITicketRepository ticketRepo;
        private readonly INewReservedTicket newTicket;
        private readonly IReservationRepository reserveRepo;

        public NewTicketBuyWithSameReservationKeyValidation(ITicketRepository ticketRepo, INewReservedTicket newTicket, IReservationRepository reserveRepo)
        {
            this.ticketRepo = ticketRepo;
            this.newTicket = newTicket;
            this.reserveRepo = reserveRepo;
        }

        public async Task<NewReservedTicketSummary> New(ITicketCreation ticket)
        {
            var reservations = await reserveRepo.GetAllReservations();
            await reserveRepo.CancelExpiredReservations(reservations);

            var ticketDb = ticketRepo.Get(ticket.Row, ticket.Column, ticket.ProjectionId);

            if (ticketDb != null)
            {
                return new NewReservedTicketSummary(false, "Cannot by reserved seats twice");
            }

            return await newTicket.New(ticket);
        }
    }
}
