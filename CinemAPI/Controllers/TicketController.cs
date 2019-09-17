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

        public TicketController(IReservationTicketRepository ticketRepo,
                                INewReservationTicket newTicket,
                                IProjectionRepository projRepo
                                )
        {
            this.ticketRepo = ticketRepo;
            this.newTicket = newTicket;
            this.projRepo = projRepo;
        }

        [HttpPost]
        [Route("api/ticket/{id}/{row}/{column}")]
        public IHttpActionResult Ticket(int id, int row, int column)
        {
            Projection dbProj = projRepo.GetById(id);

            NewReservationTicketSummary summary = newTicket.New(new ReservationTicket(dbProj.StartDate, dbProj.Movie.Name, dbProj.Room.Cinema.Name, dbProj.Room.Number, row, column, dbProj.Id));

            var ticket = ticketRepo.GetByRowAndColumn(row, column);

            if (summary.IsCreated)
            {
                projRepo.DecreaseSeatsCount(id);

                return Ok(ticket);
            }
            else
            {
                return BadRequest(summary.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult Index()
        {
            IEnumerable<IReservationTicket> reservations = ticketRepo.GetAllReservations();

            if (reservations.Any())
            {
                ticketRepo.CancelExpiredReservations(reservations);

                return this.Ok();
            }
            else
            {
                return this.BadRequest();
            }
        }
    }
}
