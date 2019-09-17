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
        private readonly IProjectionRepository projRepo;

        public ReservationTicketRepository(CinemaDbContext db, IProjectionRepository projRepo)
        {
            this.db = db;
            this.projRepo = projRepo;
        }

        public void CancelExpiredReservations(IEnumerable<IReservationTicket> reservations)
        {
            DateTime currentDate = DateTime.UtcNow;

            foreach (var reservation in reservations)
            {
                TimeSpan ts = reservation.ProjectionStartDate - currentDate;

                if (ts.TotalMinutes < 10)
                {
                    this.db.ReservationTickets.Remove(reservation as ReservationTicket);
                    this.db.SaveChanges();

                    projRepo.IncreaseSeatsCount(reservation.ProjectionId);
                }
            }
        }

        public IEnumerable<IReservationTicket> GetAllReservations()
        {
            return this.db.ReservationTickets.ToList();
        }

        public IReservationTicket GetByRowAndColumn(int row, int column)
        {
            return this.db.ReservationTickets
                .Where(x => x.Row == row && x.Column == column)
                .FirstOrDefault();
        }

        public IReservationTicket Insert(IReservationTicketCreation ticket)
        {
            ReservationTicket newTicket = new ReservationTicket(ticket.ProjectionStartDate, ticket.Movie, ticket.Cinema, ticket.Room, ticket.Row, ticket.Column, ticket.ProjectionId);

            db.ReservationTickets.Add(newTicket);
            db.SaveChanges();

            return newTicket;
        }
    }
}
