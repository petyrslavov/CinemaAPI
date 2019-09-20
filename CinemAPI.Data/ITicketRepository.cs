using CinemAPI.Models.Contracts.Ticket;
using System.Threading.Tasks;

namespace CinemAPI.Data
{
    public interface ITicketRepository
    {
        Task<ITicket> Insert(ITicketCreation ticket);

        ITicket Get(int row, int column, long projectionId);
    }
}
