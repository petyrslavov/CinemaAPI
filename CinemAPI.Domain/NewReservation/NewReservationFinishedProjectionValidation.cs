﻿using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Reservation;
using System;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewReservation
{
    public class NewReservationFinishedProjectionValidation : INewReservation
    {
        private readonly INewReservation newReservation;

        public NewReservationFinishedProjectionValidation(INewReservation newReservation)
        {
            this.newReservation = newReservation;
        }

        public async Task<NewReservationSummary> New(IReservationCreation reservation)
        {
            DateTime currentDate = DateTime.UtcNow;

            if (currentDate > reservation.ProjectionStartDate)
            {
                return new NewReservationSummary(false, "Cannot reserve seats for finished projection");
            }

            return await newReservation.New(reservation);
        }
    }
}
