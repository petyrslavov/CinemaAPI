using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using CinemAPI.Models;

namespace CinemAPI.Data.EF.ModelConfigurations
{
    internal sealed class ReservationTicketModelConfiguration : IModelConfiguration
    {
        public void Configure(DbModelBuilder modelBuilder)
        {
            EntityTypeConfiguration<ReservationTicket> projectionModel = modelBuilder.Entity<ReservationTicket>();
            projectionModel.HasKey(model => model.Id);
            projectionModel.Property(model => model.Movie).IsRequired();
            projectionModel.Property(model => model.Room).IsRequired();
            projectionModel.Property(model => model.Cinema).IsRequired();
            projectionModel.Property(model => model.ProjectionStartDate).IsRequired();
            projectionModel.Property(model => model.Row).IsRequired();
            projectionModel.Property(model => model.Column).IsRequired();
            projectionModel.Property(model => model.ProjectionId).IsRequired();

        }
    }
}
