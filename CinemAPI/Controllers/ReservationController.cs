using CinemAPI.Data;
using CinemAPI.Data.EF;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Reservation;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public IHttpActionResult Reservation(int id, int row, int column)
        {
            Projection dbProj = projRepo.GetById(id);

            NewReservationSummary summary = newReserservation.New(new Reservation(dbProj.StartDate, dbProj.Movie.Name, dbProj.Room.Cinema.Name, dbProj.Room.Number, row, column, dbProj.Id));

            var reservation = reserveRepo.Get(row, column, id);

            if (summary.IsCreated)
            {
                projRepo.DecreaseSeatsCount(id);

                return Ok(reservation);
            }
            else
            {
                return BadRequest(summary.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult Index()
        {
            IEnumerable<IReservation> reservations = reserveRepo.GetAllReservations();

            if (reservations.Any())
            {
                reserveRepo.CancelExpiredReservations(reservations);

                return this.Ok();
            }
            else
            {
                return this.BadRequest();
            }
        }
    }
}
