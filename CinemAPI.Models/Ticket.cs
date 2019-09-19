using CinemAPI.Models.Contracts.Ticket;
using System;

namespace CinemAPI.Models
{
    public class Ticket: ITicket, ITicketCreation
    {
        public Ticket()
        {

        }

        public Ticket(DateTime projectionStartDate, string movie, string cinema, int room, int row, int column, long projectionId)
        {
            this.ProjectionStartDate = projectionStartDate;
            this.Movie = movie;
            this.Cinema = cinema;
            this.Room = room;
            this.Row = row;
            this.Column = column;
            this.ProjectionId = projectionId;
        }

        public int Id { get; set; }

        public DateTime ProjectionStartDate { get; set; }

        public string Movie { get; set; }

        public string Cinema { get; set; }

        public int Room { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public long ProjectionId { get; set; }
    }
}
