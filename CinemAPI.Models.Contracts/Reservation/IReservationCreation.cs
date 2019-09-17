﻿using System;

namespace CinemAPI.Models.Contracts.Reservation
{
    public interface IReservationCreation
    {
        int Id { get; }

        DateTime ProjectionStartDate { get; }

        string Movie { get; }

        string Cinema { get; }

        int Room { get; }

        int Row { get; }

        int Column { get; }

        long ProjectionId { get; }
    }
}