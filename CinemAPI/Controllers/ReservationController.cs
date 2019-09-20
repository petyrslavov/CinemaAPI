using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models;
using CinemAPI.Models.Input.Reservation;
using System.Threading.Tasks;
using System.Web.Http;

namespace CinemAPI.Controllers
{
    public class ReservationController : ApiController
    {
        private readonly IReservationRepository reserveRepo;
        private readonly INewReservation newReserservation;
        private readonly IProjectionRepository projRepo;

        public ReservationController(IReservationRepository reserveRepo,
                                INewReservation newReservation,
                                IProjectionRepository projRepo
                                )
        {
            this.reserveRepo = reserveRepo;
            this.newReserservation = newReservation;
            this.projRepo = projRepo;
        }

        [HttpPost]
        [Route("api/reservation/{id}/{row}/{column}")]
        public async Task<IHttpActionResult> Reservation([FromUri]ReservationCreationModel model)
        {
            Projection dbProj = projRepo.GetById(model.Id);

            NewCreationSummary summary = await newReserservation.New(new Reservation(dbProj.StartDate, dbProj.Movie.Name, dbProj.Room.Cinema.Name, dbProj.Room.Number, model.Row, model.Column, dbProj.Id));

            var reservation = await reserveRepo.Get(model.Row, model.Column, model.Id);

            if (summary.IsCreated)
            {
                await projRepo.DecreaseSeatsCount(model.Id);

                return Ok(reservation);
            }
            else
            {
                return BadRequest(summary.Message);
            }
        }
    }
}
