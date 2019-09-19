using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Models.Contracts.Ticket
{
    public interface ITicket
    {
        int Id { get; }

        DateTime ProjectionStartDate { get; }

        string Movie { get; }

        string Cinema { get; }

        int Room { get; }

        int Row { get; }

        int Column { get; }

        long ProjectionId { get;  }
    }
}
