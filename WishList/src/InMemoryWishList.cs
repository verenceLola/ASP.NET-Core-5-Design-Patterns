using System;
using System.Collections.Generic;
using WishList.Interfaces;
using System.Threading.Tasks;
using System.Linq;

namespace WishList
{
    public class InMemoryWishList : IWishList
    {
        private readonly InMemoryWishListOptions _options;
        private readonly Dictionary<string, InternalItem> _items;
        public InMemoryWishList(InMemoryWishListOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _items = new Dictionary<string, InternalItem>();
        }
        public Task<WishListItem> AddOrRefreshAsync(string itemName)
        {
            var expirationTime = _options.SystemClock.UtcNow.AddSeconds(_options.ExpirationInSeconds);
            _items.
                Where(x => x.Value.Expiration < _options.SystemClock.UtcNow)
                .Select(x => x.Key)
                .ToList()
                .ForEach(key => _items.Remove(key));

            if (_items.ContainsKey(itemName))
            {
                var item = _items[itemName];
                item.Count++;
                item.Expiration = expirationTime;

                var wishListItem = new WishListItem
                {
                    Name = itemName,
                    Count = item.Count,
                    Expiration = item.Expiration
                };

                return Task.FromResult(wishListItem);
            }
            else
            {
                var item = new InternalItem
                {
                    Count = 1,
                    Expiration = expirationTime
                };
                _items.Add(itemName, item);
                var wishListItem = new WishListItem
                {
                    Name = itemName,
                    Count = item.Count,
                    Expiration = item.Expiration
                };

                return Task.FromResult(wishListItem);
            }
        }
        public Task<IEnumerable<WishListItem>> AllAsync()
        {
            var items = _items
                .Where(x => x.Value.Expiration >= _options.SystemClock.UtcNow)
                .Select(x => new WishListItem
                {
                    Name = x.Key,
                    Count = x.Value.Count,
                    Expiration = x.Value.Expiration
                })
                .OrderByDescending(x => x.Count)
                .AsEnumerable();

            return Task.FromResult(items);
        }
        private class InternalItem
        {
            public int Count { get; set; }
            public DateTimeOffset Expiration { get; set; }
        }
    }
}
