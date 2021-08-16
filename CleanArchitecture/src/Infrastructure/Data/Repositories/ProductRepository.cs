using System;
using System.Linq;
using System.Collections.Generic;
using Core.Interfaces;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _db;
        private readonly IMapper<Models.Product, Core.Entities.Product> _dataToEntityMapper;
        private readonly IMapper<Core.Entities.Product, Models.Product> _entityToDataMapper;
        public ProductRepository(
            ProductContext db,
            IMapper<Models.Product, Core.Entities.Product> dataToEntityMapper,
            IMapper<Core.Entities.Product, Models.Product> entityToDataMapper
        )
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _dataToEntityMapper = dataToEntityMapper ?? throw new ArgumentNullException(nameof(dataToEntityMapper));
            _entityToDataMapper = entityToDataMapper ?? throw new ArgumentNullException(nameof(entityToDataMapper));
        }
        public IEnumerable<Product> All() => _db.Products.Select(
            p => _dataToEntityMapper.Map(p)
        );
        public Product FindById(int productId)
        {
            var product = _db.Products.Find(productId);

            return _dataToEntityMapper.Map(product);
        }
        public void Update(Product product)
        {
            _db.Entry(product).State = EntityState.Modified;
            _db.SaveChanges();
        }
        public void Insert(Product product)
        {
            var data = _entityToDataMapper.Map(product);
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
