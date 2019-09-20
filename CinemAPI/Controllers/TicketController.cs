using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Reservation;
using CinemAPI.Models.Input.Ticket;
using System.Threading.Tasks;
using System.Web.Http;

namespace CinemAPI.Controllers
{
    public class TicketController : ApiController
    {
        private readonly IProjectionRepository projRepo;
        private readonly INewTicket newTicket;
        private readonly ITicketRepository ticketRepo;
        private readonly IReservationRepository reserveRepo;
        private readonly INewReservedTicket newReservedTicket;

        public TicketController(IProjectionRepository projRepo, INewTicket newTicket, ITicketRepository ticketRepo, IReservationRepository reserveRepo, INewReservedTicket newReservedTicket)
        {
            this.projRepo = projRepo;
            this.newTicket = newTicket;
            this.ticketRepo = ticketRepo;
            this.reserveRepo = reserveRepo;
            this.newReservedTicket = newReservedTicket;
        }


        //Buy Ticket Without Reservation
        [HttpPost]
        [Route("api/ticket/{id}/{row}/{column}")]
        public async Task<IHttpActionResult> Ticket([FromUri]TicketCreationModel model)
        {
            Projection dbProj = projRepo.GetById(model.Id);

            NewCreationSummary summary = await newTicket.New(new Ticket(dbProj.StartDate, dbProj.Movie.Name, dbProj.Room.Cinema.Name, dbProj.Room.Number, model.Row, model.Column, dbProj.Id));

            var ticket = ticketRepo.Get(model.Row, model.Column, model.Id);

            if (summary.IsCreated)
            {
                await projRepo.DecreaseSeatsCount(model.Id);

                return Ok(ticket);
            }
            else
            {
                return BadRequest(summary.Message);
            }
        }

        //Buy Ticket With Reservation
        [HttpPost]
        [Route("api/ticket/{id}")]
        public async Task<IHttpActionResult> Ticket(int id)
        {
            IReservation reservation = reserveRepo.GetById(id);

            NewCreationSummary summary = await newReservedTicket.New(new Ticket(reservation.ProjectionStartDate, reservation.Movie, reservation.Cinema, reservation.Room, reservation.Row, reservation.Column, reservation.ProjectionId));

            var ticket = ticketRepo.Get(reservation.Row, reservation.Column, reservation.ProjectionId);

            if (summary.IsCreated)
            {
                await projRepo.DecreaseSeatsCount(reservation.ProjectionId);

                return Ok(ticket);
            }
            else
            {
                return BadRequest(summary.Message);
            }
        }
    }
}
