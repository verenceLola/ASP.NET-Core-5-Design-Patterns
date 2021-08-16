using Core.Interfaces;

namespace Infrastructure.Data.Mappers
{
    public class ProductDataToEntityMapper : IMapper<Models.Product, Core.Entities.Product>
    {
        public Core.Entities.Product Map(Models.Product entity) =>
            new Core.Entities.Product
            {
                Id = entity.Id,
                QuantityInStock = entity.QuantityInStock,
                Name = entity.Name
            };
    }
    public class ProductEntityToDataMapper : IMapper<Core.Entities.Product, Models.Product>{
        public Models.Product Map(Core.Entities.Product entity) =>
            new Models.Product
            {
                Id = entity.Id,
                QuantityInStock = entity.QuantityInStock,
                Name = entity.Name
            };
    }
}
