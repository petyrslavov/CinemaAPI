using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Reservation;

namespace CinemAPI.Domain.NewReservation
{
    public class NewReservationNotExistingSeatsValidation : INewReservation
    {
        private readonly INewReservation newReservation;
        private readonly IProjectionRepository projRepo;

        public NewReservationNotExistingSeatsValidation(INewReservation newReservation, IProjectionRepository projRepo)
        {
            this.newReservation = newReservation;
            this.projRepo = projRepo;
        }

        public NewReservationSummary New(IReservationCreation reservation)
        {
            var projection = projRepo.GetById(reservation.ProjectionId);

            if (reservation.Row > projection.Room.Rows || reservation.Column > projection.Room.SeatsPerRow)
            {
                return new NewReservationSummary(false, "The seats does not exist in this room");
            }

            return newReservation.New(reservation);
        }
    }
}
