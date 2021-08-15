using System;
using System.Collections.Generic;
using Layering.Data;
using Layering.SharedModels;
using Microsoft.EntityFrameworkCore;

namespace Layering.EfCore
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _db;
        public ProductRepository(ProductContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public IEnumerable<Product> All() => _db.Products;
        public Product FindById(int productId) => _db.Products.Find(productId);
        public void Insert(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
        }
        public void DeleteById(int productId)
        {
            var product = _db.Products.Find(productId);
            _db.Products.Remove(product);
            _db.SaveChanges();
        }
        public void Update(Product product)
        {
            _db.Entry(product).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
