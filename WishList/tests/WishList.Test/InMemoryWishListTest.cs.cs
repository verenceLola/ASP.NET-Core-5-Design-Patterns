using System;
using Moq;
using System.Threading.Tasks;
using Xunit;
using WishList.Interfaces;
using WishList.Internals;

namespace WishList.Test
{
    public class InMemoryWishListTest
    {
        private readonly Mock<ISystemClock> _systemClockMock;
        private readonly InMemoryWishListOptions _options;
        private readonly IWishList sut;
        public InMemoryWishListTest()
        {
            _systemClockMock = new Mock<ISystemClock>();
            _options = new InMemoryWishListOptions
            {
                SystemClock = _systemClockMock.Object,
                ExpirationInSeconds = 30
            };
#if TEST_InMemoryWishListRefactored
            sut = new InMemoryWishListRefactored(_options);
#else
            sut = new InMemoryWishList(_options);
#endif
        }
        public class AddOrRefreshAsync : InMemoryWishListTest
        {
            [Fact]
            public async Task Should_create_new_item()
            {
                var (_, expectedExpiryTime) = SetUtcNow();
                var result = await sut.AddOrRefreshAsync("NewItem");

                Assert.Equal("NewItem", result.Name);
                Assert.Equal(1, result.Count);
                Assert.Equal(expectedExpiryTime, result.Expiration);

                var all = await sut.AllAsync();
                Assert.Collection(all, x =>
                {
                    Assert.Equal("NewItem", x.Name);
                    Assert.Equal(1, x.Count);
                    Assert.Equal(expectedExpiryTime, x.Expiration);
                });
            }
            public async Task Should_set_the_new_Expiration_date_of_an_existing_item()
            {
                await AddOrRefreshAnExpiredItemAsync("NewItem");
                var (_, expectedExpiryTime) = SetUtcNow();

                var result = await sut.AddOrRefreshAsync("NewItem");

                Assert.Equal(expectedExpiryTime, result.Expiration);
                var all = await sut.AllAsync();
                Assert.Collection(all, x => Assert.Equal(1, x.Count));
            }
            [Fact]
            public async Task Should_remove_expired_items()
            {
                await AddOrRefreshAnItemAsync("Item1");
                await AddOrRefreshAnItemAsync("Item2");
                await AddOrRefreshAnItemAsync("Item3");
                await AddOrRefreshAnExpiredItemAsync("Item4");

                await sut.AddOrRefreshAsync("Item5");

                var result = await sut.AllAsync();
                Assert.Collection(result,
                    x => Assert.Equal("Item1", x.Name),
                    x => Assert.Equal("Item2", x.Name),
                    x => Assert.Equal("Item3", x.Name),
                    x => Assert.Equal("Item5", x.Name)
                );
            }
        }
        public class AllAsync : InMemoryWishListTest
        {
            [Fact]
            public async Task Should_not_return_expired_items()
            {
                await AddOrRefreshAnItemAsync("Item1");
                await AddOrRefreshAnExpiredItemAsync("Item2");

                var result = await sut.AllAsync();

                Assert.Collection(result,
                    x => Assert.Equal("Item1", x.Name)
                );
            }
            [Fact]
            public async Task Should_return_items_ordered_by_Count_Descending()
            {
                await AddOrRefreshAnItemAsync("Item1");
                await AddOrRefreshAnItemAsync("Item1");
                await AddOrRefreshAnItemAsync("Item1");
                await AddOrRefreshAnItemAsync("Item2");
                await AddOrRefreshAnItemAsync("Item3");
                await AddOrRefreshAnItemAsync("Item3");

                var result = await sut.AllAsync();

                Assert.Collection(result,
                    x => Assert.Equal("Item1", x.Name),
                    x => Assert.Equal("Item3", x.Name),
                    x => Assert.Equal("Item2", x.Name)
                );
            }
        }
        private (DateTimeOffset UtcNow, DateTimeOffset ExpectedExpiryTime) SetUtcNow()
        {
            var utcNow = DateTimeOffset.UtcNow;
            _systemClockMock.Setup(x => x.UtcNow).Returns(utcNow);
            var expectedExpiryTime = utcNow.AddSeconds(_options.ExpirationInSeconds);

            return (utcNow, expectedExpiryTime);
        }
        private (DateTimeOffset UtcNow, DateTimeOffset ExpectedExpiryTime) SetUtcNowToExpired()
        {
            var delay = (_options.ExpirationInSeconds * 2);
            var utcNow = DateTimeOffset.UtcNow.AddSeconds(delay);
            _systemClockMock.Setup(x => x.UtcNow).Returns(utcNow);
            var expectedExpiryTime = utcNow.AddSeconds(_options.ExpirationInSeconds);

            return (utcNow, expectedExpiryTime);
        }
        private async Task AddOrRefreshAnItemAsync(string itemName)
        {
            var (_, expiredDate) = SetUtcNow();
            var item = await sut.AddOrRefreshAsync(itemName);
            Assert.Equal(expiredDate, item.Expiration);
        }
        private async Task AddOrRefreshAnExpiredItemAsync(string itemName)
        {
            var (_, firstExpiredDate) = SetUtcNowToExpired();
            var item = await sut.AddOrRefreshAsync(itemName);

            Assert.Equal(firstExpiredDate, item.Expiration);
            SetUtcNow();
        }
    }
}
