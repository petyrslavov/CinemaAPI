using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemAPI.Data.EF;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Reservation;

namespace CinemAPI.Data.Implementation
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly CinemaDbContext db;
        private readonly IProjectionRepository projRepo;

        public ReservationRepository(CinemaDbContext db, IProjectionRepository projRepo)
        {
            this.db = db;
            this.projRepo = projRepo;
        }

        public void CancelExpiredReservations(IEnumerable<IReservation> reservations)
        {
            DateTime currentDate = DateTime.UtcNow;

            foreach (var reservation in reservations)
            {
                TimeSpan ts = reservation.ProjectionStartDate - currentDate;

                if (ts.TotalMinutes < 10)
                {
                    this.db.Reservations.Remove(reservation as Reservation);
                    this.db.SaveChanges();

                    projRepo.IncreaseSeatsCount(reservation.ProjectionId);
                }
            }
        }

        public IEnumerable<IReservation> GetAllReservations()
        {
            return this.db.Reservations.ToList();
        }

        public IReservation Get(int row, int column, long projectionId)
        {
            return this.db.Reservations
                .Where(x => x.Row == row && x.Column == column && x.ProjectionId == projectionId)
                .FirstOrDefault();
        }

        public IReservation Insert(IReservationCreation reservation)
        {
            Reservation newReservation = new Reservation(reservation.ProjectionStartDate, reservation.Movie, reservation.Cinema, reservation.Room, reservation.Row, reservation.Column, reservation.ProjectionId);

            db.Reservations.Add(newReservation);
            db.SaveChanges();

            return newReservation;
        }
    }
}
