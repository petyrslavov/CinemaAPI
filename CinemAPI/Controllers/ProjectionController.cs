using CinemAPI.Data;
using CinemAPI.Data.EF;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models;
using CinemAPI.Models.Input.Projection;
using System;
using System.Linq;
using System.Web.Http;

namespace CinemAPI.Controllers
{
    public class ProjectionController : ApiController
    {
        private readonly INewProjection newProj;
        private readonly IProjectionRepository projectionRepository;
        private readonly CinemaDbContext context;

        public ProjectionController(INewProjection newProj, IProjectionRepository projectionRepository, CinemaDbContext context)
        {
            this.newProj = newProj;
            this.projectionRepository = projectionRepository;
            this.context = context;
        }

        [HttpPost]
        public IHttpActionResult Index(ProjectionCreationModel model)
        {
            NewProjectionSummary summary = newProj.New(new Projection(model.MovieId, model.RoomId, model.StartDate, model.AvailableSeatsCount));

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
            Projection projection = context.Projections.FirstOrDefault(proj => proj.Id == id);

            if (projection.StartDate > DateTime.Now)
            {
                projectionRepository.GetAvailableSeatsCount(id);
                return Ok();
            }
            else
            {
                return BadRequest("Projection is finished or alredy started");
            }
        }
    }
}