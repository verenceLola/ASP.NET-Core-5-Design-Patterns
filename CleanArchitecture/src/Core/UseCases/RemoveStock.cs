using System;
using MediatR;
using Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Core.UseCases
{
    public class RemoveStocks
    {
        public class Command : IRequest<int>
        {
            public int ProductId { get; set; }
            public int Amount { get; set; }

            public void Deconstruct(out int productId, out int amount) =>
                (productId, amount) = (ProductId, Amount);
        }
        public class Handler : IRequestHandler<Command, int>
        {
            private readonly IProductRepository _productRepository;
            public Handler(IProductRepository productRepository)
            {
                _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            }
            public async Task<int> Handle(Command command, CancellationToken cancellationToken)
            {
                var (productId, amount) = command;
                var product = await _productRepository.FindByIdAsync(productId, cancellationToken);

                if (amount > product.QuantityInStock)
                {
                    throw new Exceptions.NotEnoughStockException(product.QuantityInStock, amount);
                }

                product.QuantityInStock -= amount;
                await _productRepository.UpdateAsync(product, cancellationToken);

                return product.QuantityInStock;
            }
        }
    }
}
