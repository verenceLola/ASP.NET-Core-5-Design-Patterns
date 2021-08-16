using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Web.DTO;
using Core.Interfaces;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper<Core.Entities.Product, DTO.ProductDetails> _mapper;
        public ProductsController(IProductRepository productRepository, IMapper<Core.Entities.Product, DTO.ProductDetails> mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        public ActionResult<IEnumerable<ProductDetails>> Get()
        {
            var products = _productRepository.All().Select(
                p => _mapper.Map(p)
            );

            return Ok(products);
        }
    }
}
