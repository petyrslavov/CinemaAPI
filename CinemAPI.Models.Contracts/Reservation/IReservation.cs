﻿using System;

namespace CinemAPI.Models.Contracts.Reservation
{
    public interface IReservation
    {
        int Id { get; }

        DateTime ProjectionStartDate { get; }

        string Movie { get; }

        string Cinema { get; }

        int Room { get; }

        short Row { get; }

        short Column { get; }

        long ProjectionId { get; }
    }
}
