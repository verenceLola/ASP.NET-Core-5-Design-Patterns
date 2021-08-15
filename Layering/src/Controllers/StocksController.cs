using System;
using Layering.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Layering.DTO;
using Layering.SharedModels;

namespace Layering.Controllers
{
    [ApiController]
    [Route("products/{productId}")]
    public class StocksController : ControllerBase
    {
        private readonly IStockService _stockService;
        public StocksController(IStockService stockService)
        {
            _stockService = stockService ?? throw new ArgumentNullException(nameof(stockService));
        }

        [HttpPost("add-stocks")]
        public ActionResult<StockLevel> Add(int productId, [FromBody] AddStocksCommand command)
        {
            var quantityInStock = _stockService.AddStock(productId, command.Amount);
            var stockLevel = new StockLevel(quantityInStock);

            return Ok(stockLevel);
        }

        [HttpPost("remove-stocks")]
        public ActionResult<StockLevel> Remove(int productId, [FromBody] RemoveStocksCommand command)
        {
            try
            {
                var quantityInStock = _stockService.RemoveStock(productId, command.Amount);
                var stockLevel = new StockLevel(quantityInStock);

                return Ok(stockLevel);
            }
            catch (NotEnoughStockException ex)
            {
                return Conflict(new
                {
                    ex.Message,
                    ex.AmountToRemove,
                    ex.QuantityInStock
                });
            }
        }
    }
}
