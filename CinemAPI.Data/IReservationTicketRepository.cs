using CinemAPI.Models.Contracts.ReservationTicket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Data
{
    public interface IReservationTicketRepository
    {
        IReservationTicket Insert(IReservationTicketCreation reservationTicket);

        IReservationTicket GetByRowAndColumn(int row, int column);
    }
}
