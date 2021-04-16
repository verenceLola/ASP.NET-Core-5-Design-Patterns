using System;
using Xunit;
using Singleton;

namespace SIngletonTest
{
    public class MySingletonTest
    {
        [Fact]
        public void Create_should_always_return_the_same_instance()
        {
            var first = MySingleton.Instance;
            var second = MySingleton.Instance;

            Assert.Same(first, second);
        }
    }
}
