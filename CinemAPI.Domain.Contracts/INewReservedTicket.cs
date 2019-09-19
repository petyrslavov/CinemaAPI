using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Ticket;
using System.Threading.Tasks;

namespace CinemAPI.Domain.Contracts
{
    public interface INewReservedTicket
    {
        Task<NewReservedTicketSummary> New(ITicketCreation ticket);
    }
}
