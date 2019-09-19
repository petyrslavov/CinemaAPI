using CinemAPI.Data.EF;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Projection;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CinemAPI.Data.Implementation
{
    public class ProjectionRepository : IProjectionRepository
    {
        private readonly CinemaDbContext db;

        public ProjectionRepository(CinemaDbContext db)
        {
            this.db = db;
        }

        public async Task DecreaseSeatsCount(long projectionId)
        {
            Projection projection = await this.db.Projections.FirstOrDefaultAsync(x => x.Id == projectionId);

            projection.AvailableSeatsCount--;
            await this.db.SaveChangesAsync();
        }

        public async Task IncreaseSeatsCount(long projectionId)
        {
            Projection projection = await this.db.Projections.FirstOrDefaultAsync(x => x.Id == projectionId);

            projection.AvailableSeatsCount++;
            await this.db.SaveChangesAsync();
        }


        public IProjection Get(int movieId, int roomId, DateTime startDate)
        {
            return db.Projections.FirstOrDefault(x => x.MovieId == movieId &&
                                                      x.RoomId == roomId &&
                                                      x.StartDate == startDate);
        }

        public IEnumerable<IProjection> GetActiveProjections(int roomId)
        {
            DateTime now = DateTime.UtcNow;

            return db.Projections.Where(x => x.RoomId == roomId &&
                                             x.StartDate > now);
        }

        public int GetAvailableSeatsCount(long projectionId)
        {
            Projection projection = db.Projections.FirstOrDefault(proj => proj.Id == projectionId);

            return projection.AvailableSeatsCount;
        }

        public Projection GetById(long projectionId)
        {
            return db.Projections
                .Include(m => m.Movie)
                .Include(r => r.Room)
                .Include("Room.Cinema")
                .FirstOrDefault(proj => proj.Id == projectionId);
        }

        public void Insert(IProjectionCreation proj)
        {
            Projection newProj = new Projection(proj.MovieId, proj.RoomId, proj.StartDate, proj.AvailableSeatsCount);

            db.Projections.Add(newProj);
            db.SaveChanges();
        }
    }
}