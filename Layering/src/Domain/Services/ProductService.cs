using System;
using System.Collections.Generic;
using Layering.Data;
using System.Linq;
using Layering.SharedModels;

namespace Layering.Domain.Services
{
    public interface IProductService
    {
        IEnumerable<Product> All();
    }
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public IEnumerable<Product> All() => _repository.All().Select(
            p => new Product
            (p.Id, p.Name, p.QuantityInStock)
        );
    }
}
