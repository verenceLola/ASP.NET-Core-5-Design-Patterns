using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;
using Core.Interfaces;

namespace Core.UseCases
{
    public class AddStocks
    {
        public class Command : IRequest<int>
        {
            public int ProductId { get; set; }
            public int Amount { get; set; }
        }

        public class Handler : IRequestHandler<Command, int>
        {
            private readonly IProductRepository _productRepository;
            public Handler(IProductRepository productRepository)
            {
                _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            }
            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                var product = await _productRepository.FindByIdAsync(request.ProductId, cancellationToken);
                product.QuantityInStock += request.Amount;
                await _productRepository.UpdateAsync(product, cancellationToken);

                return product.QuantityInStock;
            }
        }
    }
}
