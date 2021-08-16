using System;
using Core.Interfaces;

namespace Core.UseCases
{
    public class RemoveStocks
    {
        private readonly IProductRepository _productRepository;
        public RemoveStocks(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }
        public Entities.Product Handle(int productId, int amount)
        {
            var product = _productRepository.FindById(productId);
            if (amount > product.QuantityInStock)
            {
                throw new Exceptions.NotEnoughStockException(product.QuantityInStock, amount);
            }
            product.QuantityInStock -= amount;
            _productRepository.Update(product);

            return product;
        }
    }
}
