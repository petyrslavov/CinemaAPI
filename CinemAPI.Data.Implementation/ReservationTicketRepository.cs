using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemAPI.Data.EF;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.ReservationTicket;

namespace CinemAPI.Data.Implementation
{
    public class ReservationTicketRepository : IReservationTicketRepository
    {
        private readonly CinemaDbContext db;

        public ReservationTicketRepository(CinemaDbContext db)
        {
            this.db = db;
        }

        public IReservationTicket GetByRowAndColumn(int row, int column)
        {
            return this.db.ReservationTickets
                .Where(x => x.Row == row && x.Column == column)
                .FirstOrDefault();
        }

        public IReservationTicket Insert(IReservationTicketCreation ticket)
        {
            ReservationTicket newTicket = new ReservationTicket(ticket.ProjectionStartDate, ticket.Movie, ticket.Cinema, ticket.Room, ticket.Row, ticket.Column);

            db.ReservationTickets.Add(newTicket);
            db.SaveChanges();

            return newTicket;
        }
    }
}
