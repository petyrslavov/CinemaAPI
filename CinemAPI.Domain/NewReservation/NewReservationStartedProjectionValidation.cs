using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Reservation;
using System;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewReservation
{
    public class NewReservationStartedProjectionValidation : INewReservation
    {
        private readonly INewReservation newReservation;

        public NewReservationStartedProjectionValidation(INewReservation newReservation)
        {
            this.newReservation = newReservation;
        }

        public async Task<NewReservationSummary> New(IReservationCreation reservation)
        {
            DateTime currentDate = DateTime.UtcNow;
            TimeSpan ts = reservation.ProjectionStartDate - currentDate;

            if (ts.TotalMinutes > 0 && ts.TotalMinutes < 10 )
            {
                return new NewReservationSummary(false, "Cannot reserve seats for projection starting in less than 10 minutes");
            }

            return await newReservation.New(reservation);
        }
    }
}
