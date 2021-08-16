using System;
using Core.UseCases;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web.DTO;
using Core.Exceptions;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StocksController : ControllerBase
    {
        private readonly IMapper _mapper;
        public StocksController(IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpPost("remove-stocks")]
        public ActionResult<StockLevel> Remove(
            int productId,
            [FromBody] RemoveStocksCommand command,
            [FromServices] RemoveStocks useCase
        )
        {
            try
            {
                var product = useCase.Handle(productId, command.Amount);
                var stockLevel = _mapper.Map<DTO.StockLevel>(product);

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
            var product = useCase.Handle(productId, command.Amount);
            var stockLevel = _mapper.Map<DTO.StockLevel>(product);

            return Ok(stockLevel);
        }
    }
}
