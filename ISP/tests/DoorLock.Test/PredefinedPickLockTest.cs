using Xunit;
using DoorLock.Exceptions;
using DoorLock.Interfaces;

namespace DoorLock.Test
{
    public class PredefinedPickLockTest
    {
        public class Pick : PredefinedPickLockTest
        {
            public static TheoryData<ILock> PickableLocks = new TheoryData<ILock>{
                new BasicLock("key1", isLocked: true),
                new MultiLock(
                    new BasicLock("key2", isLocked: true),
                    new BasicLock("key3", isLocked: true)
                ),
                new MultiLock(
                    new BasicLock("key2", isLocked: true),
                    new MultiLock(
                        new BasicLock("key1", isLocked: true),
                        new BasicLock("key3", isLocked: true)
                    )
                )
            };
            [Theory]
            [MemberData(nameof(PickableLocks))]
            public void Should_unlock_the_specified_ILock(ILock @lock)
            {
                Assert.True(@lock.IsLocked, "The lock should be locked.");
                var sut = new PredefinedPickLock(new[] { "key1", "key2", "key3" });

                sut.Pick(@lock);
                Assert.False(@lock.IsLocked, "The lock should be unlocked.");
            }
            [Fact]
            public void Should_throw_an_ImpossibleToPickTheLockException_when_the_lock_cannot_be_opened()
            {
                var sut = new PredefinedPickLock(new[] { "key2" });
                var @lock = new BasicLock("key1");

                Assert.Throws<ImpossibleToPickTheLockException>(() => sut.Pick(@lock));
            }
        }
        public class CreateMatchingKeyFor : PredefinedPickLockTest
        {
            [Fact]
            public void Should_return_the_matching_key_when_provided()
            {
                var sut = new PredefinedPickLock(new[] { "key1" });
                var @lock = new BasicLock("key1");

                var key = sut.CreateMatchingKeyFor(@lock);

                Assert.NotNull(key);
                Assert.Equal("key1", key.Signature);
            }
            [Fact]
            public void Should_throw_an_ImpossibleToPickTheLockException_when_no_matching_key_can_be_generated()
            {
                var sut = new PredefinedPickLock(new[] { "key2" });
                var @lock = new BasicLock("key1");

                Assert.Throws<ImpossibleToPickTheLockException>(() => sut.CreateMatchingKeyFor(@lock));
            }
        }
    }
}
