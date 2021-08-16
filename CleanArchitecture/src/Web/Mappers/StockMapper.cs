using Core.Entities;
using AutoMapper;

namespace Web.Mappers
{
    public class StockProfile : Profile
    {
        public StockProfile()
        {
            CreateMap<Product, DTO.StockLevel>();
        }
    }
}
