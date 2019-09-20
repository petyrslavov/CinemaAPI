using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Ticket;
using System;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewTicket
{
    public class NewTicketFinishedProjectionValidation : INewTicket
    {
        private readonly INewTicket newTicket;

        public NewTicketFinishedProjectionValidation(INewTicket newTicket)
        {
            this.newTicket = newTicket;
        }

        public async Task<NewCreationSummary> New(ITicketCreation ticket)
        {
            DateTime currentDate = DateTime.UtcNow;

            if (currentDate > ticket.ProjectionStartDate)
            {
                return new NewCreationSummary(false, "Cannot buy seats for finished projection");
            }

            return await newTicket.New(ticket);
        }
    }
}
