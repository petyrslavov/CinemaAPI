using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemAPI.Data.EF;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Ticket;

namespace CinemAPI.Data.Implementation
{
    public class TicketRepository : ITicketRepository
    {
        private readonly CinemaDbContext db;

        public TicketRepository(CinemaDbContext db)
        {
            this.db = db;
        }

        public ITicket Get(int row, int column, long projectionId)
        {
            return this.db.Tickets
               .Where(x => x.Row == row && x.Column == column && x.ProjectionId == projectionId)
               .FirstOrDefault();
        }

        public async Task<ITicket> Insert(ITicketCreation ticket)
        {
            Ticket newTicket = new Ticket(ticket.ProjectionStartDate, ticket.Movie, ticket.Cinema, ticket.Room, ticket.Row, ticket.Column, ticket.ProjectionId);

            db.Tickets.Add(newTicket);
            await db.SaveChangesAsync();

            return newTicket;
        }
    }
}
