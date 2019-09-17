using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.ReservationTicket;
using System;

namespace CinemAPI.Domain.NewReservationTicket
{
    public class NewReservationTicketCreation : INewReservationTicket
    {
        private readonly IReservationTicketRepository ticketRepo;

        public NewReservationTicketCreation(IReservationTicketRepository ticketRepo)
        {
            this.ticketRepo = ticketRepo;
        }

        public NewReservationTicketSummary New(IReservationTicketCreation ticket)
        {
            DateTime currentDate = DateTime.UtcNow;
            TimeSpan ts = ticket.ProjectionStartDate - currentDate;

            if (currentDate > ticket.ProjectionStartDate)
            {
                return new NewReservationTicketSummary(false, "Cannot reserve seats for finished projection");
            }

            if (ts.TotalMinutes < 10)
            {
                return new NewReservationTicketSummary(false, "Cannot reserve seats for projection starting in less than 10 minutes");
            }

            IReservationTicket ticketDb = ticketRepo.GetByRowAndColumn(ticket.Row, ticket.Column);

            if (ticketDb != null)
            {
                return new NewReservationTicketSummary(false, "The seats are already reserved");
            }

            ticketRepo.Insert(new ReservationTicket(ticket.ProjectionStartDate, ticket.Movie, ticket.Cinema, ticket.Room, ticket.Row, ticket.Column));

            return new NewReservationTicketSummary(true);
        }
    }
}
