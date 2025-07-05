using System.Collections.Generic;
using System.Threading.Tasks;

namespace CharityDonationManager.Interfaces
{
    public interface IRepository<T>
    {
        Task AddAsync(T item);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
