using Core.UseCases;
using Microsoft.AspNetCore.Mvc;
using Web.DTO;
using Core.Exceptions;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StocksController : ControllerBase
    {
        [HttpPost("remove-stocks")]
        public ActionResult<StockLevel> Remove(
            int productId,
            [FromBody] RemoveStocksCommand command,
            [FromServices] RemoveStocks useCase
        )
        {
            try
            {
                var quantityInStock = useCase.Handle(productId, command.Amount);
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
        [HttpPost("add-stocks")]
        public ActionResult<StockLevel> AddStock(
            int productId,
            [FromBody] AddStocksCommand command,
            [FromServices] AddStocks useCase
        )
        {
            var quantityInStock = useCase.Handle(productId, command.Amount);

            return Ok(new StockLevel(quantityInStock));
        }
    }
}
