using System;

namespace CinemAPI.Models.Contracts.Ticket
{
    public interface ITicketCreation
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
