using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Ticket;
using System;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewReservedTicket
{
    public class NewTicketWithReservatio10MinBeforeProjStartValidation: INewReservedTicket
    {
        private readonly INewReservedTicket newTicket;

        public NewTicketWithReservatio10MinBeforeProjStartValidation(INewReservedTicket newTicket)
        {
            this.newTicket = newTicket;
        }

        public async Task<NewReservedTicketSummary> New(ITicketCreation ticket)
        {
            DateTime currentDate = DateTime.UtcNow;
            TimeSpan ts = ticket.ProjectionStartDate - currentDate;

            if (ts.TotalMinutes > 0 && ts.TotalMinutes < 10)
            {
                return new NewReservedTicketSummary(false, "Cannot buy seats with reservation for projection starting in less than 10 minutes");
            }

            return await newTicket.New(ticket);
        }
    }
}
