using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Reservation;

namespace CinemAPI.Domain.NewReservation
{
    public class NewReservationCreation : INewReservation
    {
        private readonly IReservationRepository reserveRepo;

        public NewReservationCreation(IReservationRepository reserveRepo)
        {
            this.reserveRepo = reserveRepo;
        }

        public NewReservationSummary New(IReservationCreation reservation)
        {
            reserveRepo.Insert(new Reservation(reservation.ProjectionStartDate, reservation.Movie, reservation.Cinema, reservation.Room, reservation.Row, reservation.Column, reservation.ProjectionId));

            return new NewReservationSummary(true);
        }
    }
}
