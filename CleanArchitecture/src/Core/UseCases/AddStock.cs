using System;
using Core.Interfaces;

namespace Core.UseCases
{
    public class AddStocks
    {
        private readonly IProductRepository _productRepository;
        public AddStocks(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }
        public int Handle(int productId, int amount)
        {
            var product = _productRepository.FindById(productId);
            product.QuantityInStock += amount;
            _productRepository.Update(product);

            return product.QuantityInStock;
        }
    }
}
