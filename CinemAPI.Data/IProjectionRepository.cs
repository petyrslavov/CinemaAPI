using CinemAPI.Models;
using CinemAPI.Models.Contracts.Projection;
using System;
using System.Collections.Generic;

namespace CinemAPI.Data
{
    public interface IProjectionRepository
    {
        IProjection Get(int movieId, int roomId, DateTime startDate);

        void Insert(IProjectionCreation projection);

        IEnumerable<IProjection> GetActiveProjections(int roomId);

        int GetAvailableSeatsCount(long projectionId);

        Projection GetById(long projectionId);

        void DecreaseSeatsCount(long projectionId);

        void IncreaseSeatsCount(long projectionId);
    }
}