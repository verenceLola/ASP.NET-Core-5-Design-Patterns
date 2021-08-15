using System;
using Xunit;
using Layering.SharedModels;

namespace tests
{
    public class ProductTest
    {
        public class AddStock : ProductTest
        {
            [Fact]
            public void Should_add_the_specified_amount_to_QunatiryInStock()
            {
                var sut = new Product("Product 1", quantityInStock: 1);
                sut.AddStock(2);

                Assert.Equal(3, sut.QuantityInStock);
            }
        }

        public class RemoveStock : ProductTest
        {
            [Fact]
            public void Should_remove_the_specified_amount_to_QuantityInStock()
            {
                var sut = new Product("Product 1", quantityInStock: 5);
                sut.RemoveStock(2);
                Assert.Equal(3, sut.QuantityInStock);
            }
            [Fact]
            public void Should_throw_a_NotEnoughStockException_when_the_specified_amount_of_items_to_remove_is_greater_than_QuantityInStock()
            {
                var sut = new Product("Product 1", quantityInStock: 2);
                var stockExecption = Assert.Throws<NotEnoughStockException>(() => sut.RemoveStock(3));
                Assert.Equal(2, stockExecption.QuantityInStock);
                Assert.Equal(3, stockExecption.AmountToRemove);
            }
        }
    }
}
