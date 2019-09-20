using CinemAPI.Data;
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
        private readonly IReservationRepository reserveRepo;

        public NewTicketStartedProjectionValidation(INewTicket newTicket, IReservationRepository reserveRepo)
        {
            this.newTicket = newTicket;
            this.reserveRepo = reserveRepo;
        }

        public async Task<NewCreationSummary> New(ITicketCreation ticket)
        {
            var reservations =  await reserveRepo.GetAllReservations();
            await reserveRepo.CancelExpiredReservations(reservations);

            DateTime currentDate = DateTime.UtcNow;
            TimeSpan ts = ticket.ProjectionStartDate - currentDate;

            if (ts.TotalMinutes > 0 && ts.TotalMinutes < 10 )
            {
                return new NewCreationSummary(false, "Cannot buy seats for projection starting in less than 10 minutes");
            }

            return await newTicket.New(ticket);
        }
    }
}
