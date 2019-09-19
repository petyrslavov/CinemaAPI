using CinemAPI.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace CinemAPI.Data.EF.ModelConfigurations
{
    internal sealed class TicketModelConfiguration : IModelConfiguration
    {
        public void Configure(DbModelBuilder modelBuilder)
        {
            EntityTypeConfiguration<Ticket> ticketModel = modelBuilder.Entity<Ticket>();
            ticketModel.HasKey(model => model.Id);
            ticketModel.Property(model => model.Movie).IsRequired();
            ticketModel.Property(model => model.Room).IsRequired();
            ticketModel.Property(model => model.Cinema).IsRequired();
            ticketModel.Property(model => model.ProjectionStartDate).IsRequired();
            ticketModel.Property(model => model.Row).IsRequired();
            ticketModel.Property(model => model.Column).IsRequired();
            ticketModel.Property(model => model.ProjectionId).IsRequired();

        }
    }
}
