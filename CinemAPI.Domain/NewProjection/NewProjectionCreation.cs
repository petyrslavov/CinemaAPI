using CinemAPI.Data;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Projection;

namespace CinemAPI.Domain
{
    public class NewProjectionCreation : INewProjection
    {
        private readonly IProjectionRepository projectionsRepo;

        public NewProjectionCreation(IProjectionRepository projectionsRepo)
        {
            this.projectionsRepo = projectionsRepo;
        }

        public NewCreationSummary New(IProjectionCreation projection)
        {
            if (projection.AvailableSeatsCount < 0)
            {
                return new NewCreationSummary(false, "Available seats count cannot be negative number");
            }

            projectionsRepo.Insert(new Projection(projection.MovieId, projection.RoomId, projection.StartDate, projection.AvailableSeatsCount));

            return new NewCreationSummary(true);
        }
    }
}