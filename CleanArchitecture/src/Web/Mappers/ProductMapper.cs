using Core.Entities;
using Core.Interfaces;

namespace Web.Mappers
{
    public class ProductMapper : IMapper<Product, DTO.ProductDetails>
    {
        public DTO.ProductDetails Map(Product entity) =>
            new DTO.ProductDetails(
                id: entity.Id,
                name: entity.Name,
                quantityInStock: entity.QuantityInStock
            );
    }
}
