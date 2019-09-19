﻿using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Ticket;
using System.Threading.Tasks;

namespace CinemAPI.Domain.NewTicket
{
    public class NewTicketCreation : INewTicket
    {
        private readonly ITicketRepository ticketRepo;

        public NewTicketCreation(ITicketRepository ticketRepo)
        {
            this.ticketRepo = ticketRepo;
        }

        public async Task<NewTicketSummary> New(ITicketCreation ticket)
        {
            await ticketRepo.Insert(new Ticket(ticket.ProjectionStartDate, ticket.Movie, ticket.Cinema, ticket.Room, ticket.Row, ticket.Column, ticket.ProjectionId));

            return new NewTicketSummary(true);
        }
    }
}
