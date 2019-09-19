using CinemAPI.Domain;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.NewProjection;
using CinemAPI.Domain.NewReservation;
using CinemAPI.Domain.NewReservedTicket;
using CinemAPI.Domain.NewTicket;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace CinemAPI.IoCContainer
{
    public class DomainPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<INewProjection, NewProjectionCreation>();
            container.RegisterDecorator<INewProjection, NewProjectionMovieValidation>();
            container.RegisterDecorator<INewProjection, NewProjectionUniqueValidation>();
            container.RegisterDecorator<INewProjection, NewProjectionRoomValidation>();
            container.RegisterDecorator<INewProjection, NewProjectionPreviousOverlapValidation>();
            container.RegisterDecorator<INewProjection, NewProjectionNextOverlapValidation>();

            container.Register<INewReservation, NewReservationCreation>();
            container.RegisterDecorator<INewReservation, NewReservationFinishedProjectionValidation>();
            container.RegisterDecorator<INewReservation, NewReservationNotExistingSeatsValidation>();
            container.RegisterDecorator<INewReservation, NewReservationStartedProjectionValidation>();
            container.RegisterDecorator<INewReservation, NewReservationReservedSeatsValidation>();

            container.Register<INewTicket, NewTicketCreation>();
            container.RegisterDecorator<INewTicket, NewTicketFinishedProjectionValidation>();
            container.RegisterDecorator<INewTicket, NewTicketStartedProjectionValidation>();
            container.RegisterDecorator<INewTicket, NewTicketBoughtSeatsValidation>();
            container.RegisterDecorator<INewTicket, NewTicketReservedSeatsValidation>();

            container.Register<INewReservedTicket, NewReservedTicketCreation>();
            container.RegisterDecorator<INewReservedTicket, NewTicketBuySeatsWithReservationValidation>();
            container.RegisterDecorator<INewReservedTicket, NewTicketBuyWithSameReservationKeyValidation>();
            container.RegisterDecorator<INewReservedTicket, NewTicketWithReservatio10MinBeforeProjStartValidation>();
        }
    }
}