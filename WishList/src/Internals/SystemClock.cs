using System;

namespace WishList.Internals {
    public class SystemClock : ISystemClock {
        public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
    }
}
