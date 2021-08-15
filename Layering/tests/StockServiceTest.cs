using Xunit;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Layering.Domain.Services;
using Layering.EfCore;
using Layering.SharedModels;


namespace tests
{
    public class StockServiceTest
    {
        private readonly DbContextOptionsBuilder _builder = new DbContextOptionsBuilder<ProductContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .ConfigureWarnings(builder => builder.Ignore(InMemoryEventId.TransactionIgnoredWarning));

        public class AddStock : StockServiceTest
        {
            [Fact]
            public void Should_add_the_specified_amount_to_QunatityInStock()
            {
                using var context = new ProductContext(_builder.Options);

                var repository = new ProductRepository(context);

                repository.Insert(new("Product 1", 1));

                var sut = new StockService(repository);
                var quantityInStock = sut.AddStock(productId: 1, amount: 2);

                Assert.Equal(3, quantityInStock);
                var actual = repository.FindById(1);
                Assert.Equal(3, actual.QuantityInStock);
            }
        }
        public class RemoveStock : StockServiceTest
        {
            [Fact]
            public void Should_remove_the_specified_amount_to_QuantityInStock()
            {
                using var context = new ProductContext(_builder.Options);

                var repository = new ProductRepository(context);

                repository.Insert(new("Product 1", 5));

                var sut = new StockService(repository);

                var quantityInStock = sut.RemoveStock(productId: 1, amount: 2);

                Assert.Equal(3, quantityInStock);
                var actual = repository.FindById(1);
                Assert.Equal(3, actual.QuantityInStock);
            }
        }
        [Fact]
        public void Should_throw_a_NotEnoughStockException_when_the_specified_amount_of_items_to_remove_is_greater_than_QuantityInStock()
        {
            using var context = new ProductContext(_builder.Options);
            var repository = new ProductRepository(context);

            repository.Insert(new(name: "Product 1", quantityInStock: 2, id: 1));

            var sut = new StockService(repository);

            var stockExecption = Assert.Throws<NotEnoughStockException>(() => sut.RemoveStock(1,3));
            Assert.Equal(2, stockExecption.QuantityInStock);
            Assert.Equal(3, stockExecption.AmountToRemove);
        }
    }
}
