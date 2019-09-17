using CinemAPI.Domain;
using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.NewProjection;
using CinemAPI.Domain.NewReservation;
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
        }
    }
}