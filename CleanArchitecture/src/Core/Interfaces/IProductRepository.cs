using System.Collections.Generic;
using System.Threading;
using Core.Entities;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> AllAsync(CancellationToken cancellationToken);
        Task<Product> FindByIdAsync(int productId, CancellationToken cancellationToken);
        Task UpdateAsync(Product product, CancellationToken cancellationToken);
        Task InsertAsync(Product product, CancellationToken cancellationToken);
        Task DeleteByIdAsync(int productId, CancellationToken cancellationToken);
    }
}
