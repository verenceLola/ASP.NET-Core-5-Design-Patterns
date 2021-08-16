using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Web.DTO;
using Core.Interfaces;
using AutoMapper;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductsController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        public ActionResult<IEnumerable<ProductDetails>> Get()
        {
            var products = _productRepository.All().Select(
                p => _mapper.Map< DTO.ProductDetails>(p)
            );

            return Ok(products);
        }
    }
}
