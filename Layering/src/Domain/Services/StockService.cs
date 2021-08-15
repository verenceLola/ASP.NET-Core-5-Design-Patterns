using System;
using Layering.Data;

namespace Layering.Domain.Services
{
    public interface IStockService
    {
        int AddStock(int productId, int amount);
        int RemoveStock(int productId, int amount);
    }

    public class StockService : IStockService
    {
        private readonly IProductRepository _repository;
        public StockService(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public int AddStock(int productId, int amount)
        {
            var product = _repository.FindById(productId);
            product.AddStock(amount);
            _repository.Update(product);

            return product.QuantityInStock;
        }
        public int RemoveStock(int productId, int amount)
        {
            var product = _repository.FindById(productId);
            product.RemoveStock(amount);
            _repository.Update(product);

            return product.QuantityInStock;
        }
    }
}
