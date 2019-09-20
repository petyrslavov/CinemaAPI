using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public async Task CancelExpiredReservations(IEnumerable<IReservation> reservations)
        {
            DateTime currentDate = DateTime.UtcNow;

            foreach (var reservation in reservations)
            {
                TimeSpan ts = reservation.ProjectionStartDate - currentDate;

                if (ts.TotalMinutes < 10)
                {
                    this.db.Reservations.Remove(reservation as Reservation);
                    await this.db.SaveChangesAsync();

                    await projRepo.IncreaseSeatsCount(reservation.ProjectionId);
                }
            }
        }

        public async Task<IEnumerable<IReservation>> GetAllReservations()
        {
            return await this.db.Reservations.ToListAsync();
        }

        public async Task<IReservation> Get(int row, int column, long projectionId)
        {
            return await this.db.Reservations
                .Where(x => x.Row == row && x.Column == column && x.ProjectionId == projectionId)
                .FirstOrDefaultAsync();
        }

        public async Task<IReservation> Insert(IReservationCreation reservation)
        {
            Reservation newReservation = new Reservation(reservation.ProjectionStartDate, reservation.Movie, reservation.Cinema, reservation.Room, reservation.Row, reservation.Column, reservation.ProjectionId);

            db.Reservations.Add(newReservation);
            await db.SaveChangesAsync();

            return newReservation;
        }

        public IReservation GetById(int reservationId)
        {
            return this.db.Reservations.FirstOrDefault(x => x.Id == reservationId);
        }
    }
}
