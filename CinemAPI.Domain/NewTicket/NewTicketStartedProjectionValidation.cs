using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Ticket;
using System;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewTicket
{
    public class NewTicketStartedProjectionValidation : INewTicket
    {
        private readonly INewTicket newTicket;

        public NewTicketStartedProjectionValidation(INewTicket newTicket)
        {
            this.newTicket = newTicket;
        }

        public async Task<NewTicketSummary> New(ITicketCreation ticket)
        {
            DateTime currentDate = DateTime.UtcNow;
            TimeSpan ts = ticket.ProjectionStartDate - currentDate;

            if (ts.TotalMinutes > 0 && ts.TotalMinutes < 10 )
            {
                return new NewTicketSummary(false, "Cannot buy seats for projection starting in less than 10 minutes");
            }

            return await newTicket.New(ticket);
        }
    }
}
