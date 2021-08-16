using System;
using System.Linq;
using System.Collections.Generic;
using Core.Interfaces;
using Core.Entities;

namespace Infrastructure.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _db;
        private readonly IMapperService _mapperService;
        public ProductRepository(
            ProductContext db,
            IMapperService mapperService
        )
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapperService = mapperService ?? throw new ArgumentNullException(nameof(mapperService));
        }
        public IEnumerable<Product> All() => _db.Products.Select(
            p => _mapperService.Map<Models.Product, Product>(p)
        );
        public Product FindById(int productId)
        {
            var product = _db.Products.Find(productId);

            return _mapperService.Map<Models.Product, Product>(product);
        }
        public void Update(Product product)
        {
            var data = _db.Products.Find(product.Id);
            data.Name = product.Name;
            data.QuantityInStock = product.QuantityInStock;
            _db.SaveChanges();
        }
        public void Insert(Product product)
        {
            var data = _mapperService.Map<Product, Models.Product>(product);
            _db.Products.Add(data);
            _db.SaveChanges();
        }
        public void DeleteById(int productId)
        {
            var product = _db.Products.Find(productId);
            _db.Products.Remove(product);
            _db.SaveChanges();
        }
    }
}
