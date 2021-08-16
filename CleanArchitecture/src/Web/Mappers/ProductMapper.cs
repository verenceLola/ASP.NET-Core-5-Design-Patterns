using Core.Entities;
using AutoMapper;

namespace Web.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile(){
            CreateMap<Product, DTO.ProductDetails>();
        }
    }
}
