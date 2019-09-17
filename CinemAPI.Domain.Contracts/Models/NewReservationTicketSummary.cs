using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Domain.Contracts.Models
{
    public class NewReservationTicketSummary
    {
        public NewReservationTicketSummary(bool isCreated)
        {
            this.IsCreated = isCreated;
        }

        public NewReservationTicketSummary(bool status, string msg)
            : this(status)
        {
            this.Message = msg;
        }

        public string Message { get; set; }

        public bool IsCreated { get; set; }
    }
}
