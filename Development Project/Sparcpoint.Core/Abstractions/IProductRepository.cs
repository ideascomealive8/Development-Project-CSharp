using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Sparcpoint.Models;

namespace Sparcpoint
{
    public interface IProductRepository
    {
        Task<Product> FindByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Product>> GetByNameAsync(string nameContains, CancellationToken cancellationToken = default);
        Task<List<Product>> GetByFilterAsync(FilterProduct filter, CancellationToken cancellationToken = default);
        Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Product> AddAsync(Product product, CancellationToken cancellationToken = default);
    }
}