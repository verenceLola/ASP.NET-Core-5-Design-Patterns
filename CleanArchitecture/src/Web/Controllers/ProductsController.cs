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
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }
        [HttpGet]
        public ActionResult<IEnumerable<ProductDetails>> Get()
        {
            var products = _productRepository.All().Select(
                p => new ProductDetails(
                    id: p.Id,
                    name: p.Name,
                    quantityInStock: p.QuantityInStock
                )
            );

            return Ok(products);
        }
    }
}
