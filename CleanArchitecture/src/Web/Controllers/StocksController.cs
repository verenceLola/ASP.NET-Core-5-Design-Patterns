using System.Threading.Tasks;
using System.Threading;
using System;
using Core.UseCases;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web.DTO;
using Core.Exceptions;
using MediatR;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StocksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public StocksController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        [HttpPost("remove-stocks")]
        public async Task<ActionResult<StockLevel>> Remove(
            int productId,
            [FromBody] RemoveStocks.Command command,
            CancellationToken cancellationToken
        )
        {
            try
            {
                command.ProductId = productId;
                var product = await _mediator.Send(command, cancellationToken);
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
        public async Task<ActionResult<StockLevel>> AddStockAsync(
            int productId,
            [FromBody] AddStocks.Command command,
            CancellationToken cancellationToken
        )
        {
            command.ProductId = productId;
            var product = await _mediator.Send(command, cancellationToken);
            var stockLevel = _mapper.Map<DTO.StockLevel>(product);

            return Ok(stockLevel);
        }
    }
}
