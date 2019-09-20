using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Projection;
using CinemAPI.Models.Input.Projection;
using System;
using System.Web.Http;

namespace CinemAPI.Controllers
{
    public class ProjectionController : ApiController
    {
        private readonly INewProjection newProj;
        private readonly IProjectionRepository projRepo;

        public ProjectionController(INewProjection newProj, IProjectionRepository projRepo)
        {
            this.newProj = newProj;
            this.projRepo = projRepo;
        }

        [HttpPost]
        public IHttpActionResult Index(ProjectionCreationModel model)
        {
            NewCreationSummary summary = newProj.New(new Projection(model.MovieId, model.RoomId, model.StartDate, model.AvailableSeatsCount));

            if (summary.IsCreated)
            {
                return Ok();
            }
            else
            {
                return BadRequest(summary.Message);
            }
        }
        
        [HttpGet]
        [Route("api/projection/seats/{id}")]
        public IHttpActionResult Seats(long id)
        {
            IProjection projection = projRepo.GetById(id);

            if (projection.StartDate > DateTime.Now)
            {
                int availableSeatsCount = projRepo.GetAvailableSeatsCount(id);
                return Ok(availableSeatsCount);
            }
            else
            {
                return BadRequest("Projection is finished or already started");
            }
        }
    }
}