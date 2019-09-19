using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Reservation;
using CinemAPI.Models.Contracts.Ticket;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewTicket
{
    public class NewTicketBoughtSeatsValidation : INewTicket
    {
        private readonly ITicketRepository ticketRepo;
        private readonly INewTicket newTicket;

        public NewTicketBoughtSeatsValidation(ITicketRepository ticketRepo, INewTicket newTicket)
        {
            this.ticketRepo = ticketRepo;
            this.newTicket = newTicket;
        }

        public async Task<NewTicketSummary> New(ITicketCreation ticket)
        {
            ITicket ticketDb = ticketRepo.Get(ticket.Row, ticket.Column, ticket.ProjectionId);

            if (ticketDb != null)
            {
                return new NewTicketSummary(false, "The seats are already bought");
            }

            return await newTicket.New(ticket);
        }
    }
}
