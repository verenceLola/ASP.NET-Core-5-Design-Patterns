using Core.Entities;
using Core.Interfaces;

namespace Web.Mappers
{
    public class StockMapper : IMapper<Product, DTO.StockLevel>
    {
        public DTO.StockLevel Map(Product entity) => new DTO.StockLevel(entity.QuantityInStock);
    }
}
