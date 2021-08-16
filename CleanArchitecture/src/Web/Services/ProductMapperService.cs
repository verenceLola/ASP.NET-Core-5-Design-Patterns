using System;
using Core.Interfaces;
using Core.Entities;

namespace Web.Services
{
    public interface IProductMapperService : IMapper<Product, DTO.ProductDetails> { }
    public class ProductMapperService : IProductMapperService
    {
        public ProductMapperService(IMapper<Product, DTO.ProductDetails> entityToDTO)
        {
            _entityToDTO = entityToDTO ?? throw new ArgumentNullException(nameof(entityToDTO));
        }
        private readonly IMapper<Product, DTO.ProductDetails> _entityToDTO;
        public DTO.ProductDetails Map(Product entity) => _entityToDTO.Map(entity);
    }
    
}
