using System;

namespace WishList
{
    public class WishListItem
    {
        public int Count { get; set; }
        public DateTimeOffset Expiration { get; set; }
        public string Name { get; set; }
    }
}
