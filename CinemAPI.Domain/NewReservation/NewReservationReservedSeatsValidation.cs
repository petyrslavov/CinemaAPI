﻿using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Reservation;

namespace CinemAPI.Domain.NewReservation
{
    public class NewReservationReservedSeatsValidation : INewReservation
    {
        private readonly IReservationRepository reserveRepo;
        private readonly INewReservation newReservation;

        public NewReservationReservedSeatsValidation(IReservationRepository reserveRepo, INewReservation newReservation)
        {
            this.reserveRepo = reserveRepo;
            this.newReservation = newReservation;
        }

        public NewReservationSummary New(IReservationCreation reservation)
        {
            IReservation reservationDb = reserveRepo.Get(reservation.Row, reservation.Column, reservation.ProjectionId);

            if (reservationDb != null)
            {
                return new NewReservationSummary(false, "The seats are already reserved");
            }

            return newReservation.New(reservation);
        }
    }
}