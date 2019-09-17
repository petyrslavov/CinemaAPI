using CinemAPI.Models.Contracts.ReservationTicket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemAPI.Models
{
    public class ReservationTicket: IReservationTicket, IReservationTicketCreation
    {
        public ReservationTicket()
        {

        }

        public ReservationTicket(DateTime projectionStartDate, string movie, string cinema, int room, int row, int column)
        {
            this.ProjectionStartDate = projectionStartDate;
            this.Movie = movie;
            this.Cinema = cinema;
            this.Room = room;
            this.Row = row;
            this.Column = column;
        }

        public int Id { get; set; }

        public DateTime ProjectionStartDate { get; set; }

        public string Movie { get; set; }

        public string Cinema { get; set; }

        public int Room { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }
    }
}
