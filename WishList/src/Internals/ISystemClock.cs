using System;

namespace WishList.Internals
{
    public interface ISystemClock
    {
        public DateTimeOffset UtcNow { get; }
    }
}
