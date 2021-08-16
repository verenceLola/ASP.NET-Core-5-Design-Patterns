
using AutoMapper;

namespace Infrastructure.Data.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Models.Product, Core.Entities.Product>().ReverseMap();
        }
    }
}
