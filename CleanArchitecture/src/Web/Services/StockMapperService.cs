using System;
using Core.Interfaces;
using Core.Entities;

namespace Web.Services
{
    public interface IStockMapperService : IMapper<Product, DTO.StockLevel> { }
    public class StockMapperService : IStockMapperService
    {
        public StockMapperService(IMapper<Product, DTO.StockLevel> entityToDTO)
        {
            _entityToDTO = entityToDTO ?? throw new ArgumentNullException(nameof(entityToDTO));
        }
        private readonly IMapper<Product, DTO.StockLevel> _entityToDTO;
        public DTO.StockLevel Map(Product product) => _entityToDTO.Map(product);
    }
}
