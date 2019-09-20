using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models.Contracts.Ticket;
using System.Threading.Tasks;

namespace CinemAPI.Domain.Contracts
{
    public interface INewTicket
    {
        Task<NewCreationSummary> New(ITicketCreation ticket);
    }
}
