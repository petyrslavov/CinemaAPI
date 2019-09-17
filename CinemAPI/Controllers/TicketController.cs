using CinemAPI.Data;
using CinemAPI.Data.EF;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.ReservationTicket;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CinemAPI.Controllers
{
    public class TicketController : ApiController
    {
        private readonly IReservationTicketRepository ticketRepo;
        private readonly INewReservationTicket newTicket;
        private readonly IProjectionRepository projRepo;
        private readonly CinemaDbContext db;

        public TicketController(IReservationTicketRepository ticketRepo,
                                INewReservationTicket newTicket,
                                IProjectionRepository projRepo,
                                CinemaDbContext db)
        {
            this.ticketRepo = ticketRepo;
            this.newTicket = newTicket;
            this.projRepo = projRepo;
            this.db = db;
        }

        [HttpPost]
        [Route("api/ticket/{id}/{row}/{column}")]
        public IHttpActionResult Ticket(int id, int row, int column)
        {
            Projection dbProj = projRepo.GetById(id);

            NewReservationTicketSummary summary = newTicket.New(new ReservationTicket(dbProj.StartDate, dbProj.Movie.Name, dbProj.Room.Cinema.Name, dbProj.Room.Number, row, column));

            var ticket = ticketRepo.GetByRowAndColumn(row, column);

            if (summary.IsCreated)
            {
                dbProj.AvailableSeatsCount--;
                db.SaveChanges();
               
                return Ok(ticket);
            }
            else
            {
                return BadRequest(summary.Message);
            }
        }
    }
}
