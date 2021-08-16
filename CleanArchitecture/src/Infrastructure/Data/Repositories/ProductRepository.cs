using System;
using System.Collections.Generic;
using Core.Interfaces;
using Core.Entities;
using AutoMapper;

namespace Infrastructure.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _db;
        private readonly IMapper _mapper;
        public ProductRepository(
            ProductContext db,
            IMapper mapper
        )
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public IEnumerable<Product> All() => _mapper.ProjectTo<Product>(_db.Products);
        public Product FindById(int productId)
        {
            var product = _db.Products.Find(productId);

            return _mapper.Map<Product>(product);
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
            var data = _mapper.Map<Models.Product>(product);
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
