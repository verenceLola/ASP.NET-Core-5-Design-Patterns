using System;
using System.Collections.Generic;
using Core.Interfaces;
using Core.Entities;
using AutoMapper;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IEnumerable<Product>> AllAsync(CancellationToken cancellationToken)
        {
            var products = await _db.Products.ToArrayAsync(cancellationToken);

            return _mapper.ProjectTo<Product>(products.AsQueryable());
        }
        public async Task<Product> FindByIdAsync(int productId, CancellationToken cancellationToken)
        {
            var product = await _db.Products.FindAsync(productId, cancellationToken);

            return _mapper.Map<Product>(product);
        }
        public async Task UpdateAsync(Product product, CancellationToken cancellationToken)
        {
            var data = await _db.Products.FindAsync(product.Id, cancellationToken);
            data.Name = product.Name;
            data.QuantityInStock = product.QuantityInStock;
            await _db.SaveChangesAsync(cancellationToken);
        }
        public async Task InsertAsync(Product product, CancellationToken cancellationToken)
        {
            var data = _mapper.Map<Models.Product>(product);
            _db.Products.Add(data);
            await _db.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteByIdAsync(int productId, CancellationToken cancellationToken)
        {
            var product = await _db.Products.FindAsync(productId, cancellationToken);
            _db.Products.Remove(product);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
