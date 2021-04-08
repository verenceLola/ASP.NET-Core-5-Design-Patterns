using System;
using Xunit;
using DoorLock.Interfaces;

namespace DoorLock.Test
{
    public class BasicLockTest
    {
        private readonly IKey _workingKey;
        private readonly IKey _invalidKey;
        private readonly BasicLock sut;
        public BasicLockTest()
        {
            sut = new BasicLock("WorkingKey");
            _invalidKey = new BasicKey("invalidKey");
            _workingKey = new BasicKey("WorkingKey");
        }
        public class DoesMatch : BasicLockTest
        {
            [Fact]
            public void Should_return_true_when_the_key_matches_the_lock()
            {
                Console.WriteLine(_workingKey.Signature);
                var result = sut.DoesMatch(_workingKey);
                Assert.True(result, "The key should match the lock.");
            }
            [Fact]
            public void Should_return_false_when_the_key_does_not_match_the_lock()
            {
                var result = sut.DoesMatch(_invalidKey);
                Assert.False(result, "The key should not match the lock.");
            }
        }
        public class Lock : BasicLockTest
        {
            public Lock()
            {
                Assert.False(sut.IsLocked, "The lock should be unlocked");
            }
            [Fact]
            public void Should_lock_the_lock_when_the_key_matches_the_lock()
            {
                sut.Lock(_workingKey);
                Assert.True(sut.IsLocked, "The lock should be locked");
            }
            [Fact]
            public void Should_throw_a_KeyDoesNotMatchException_when_the_key_does_not_match_the_lock()
            {
                Assert.Throws<KeyDoesNotMatchException>(() => sut.Lock(_invalidKey));
            }
        }
        public class Unlock : BasicLockTest
        {
            public Unlock()
            {
                sut.Lock(_workingKey);
                Assert.True(sut.IsLocked, "The lock should be unlocked");
            }
            [Fact]
            public void Should_unlock_the_lock_when_the_key_matches_the_lock()
            {
                sut.UnLock(_workingKey);
                Assert.False(sut.IsLocked, "The lock should be unlocked.");
            }
            [Fact]
            public void Should_throw_a_KeyDoesNotMatchException_when_the_key_does_not_match_the_lock()
            {
                Assert.Throws<KeyDoesNotMatchException>(() => sut.UnLock(_invalidKey));
            }
        }
    }
}
