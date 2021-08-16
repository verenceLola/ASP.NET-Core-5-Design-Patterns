using System.Collections.Generic;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> All();
        Product FindById(int productId);
        void Update(Product product);
        void Insert(Product product);
        void DeleteById(int productId);
    }
}
