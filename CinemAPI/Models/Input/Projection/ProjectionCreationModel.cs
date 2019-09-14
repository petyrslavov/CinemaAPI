using System;
using System.ComponentModel.DataAnnotations;

namespace CinemAPI.Models.Input.Projection
{
    public class ProjectionCreationModel
    {
        public int RoomId { get; set; }

        public int MovieId { get; set; }

        public DateTime StartDate { get; set; }

        [Range(0, int.MaxValue)]
        public int AvailableSeatsCount { get; set; }
    }
}