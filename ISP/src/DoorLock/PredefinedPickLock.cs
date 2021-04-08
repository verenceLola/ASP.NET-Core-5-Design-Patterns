using System;
using DoorLock.Interfaces;
using DoorLock.Exceptions;

namespace DoorLock
{
    public class PredefinedPickLock : IPickLock
    {
        private readonly string[] _signatures;
        public PredefinedPickLock(string[] signatures)
        {
            _signatures = signatures ?? throw new ArgumentNullException(nameof(signatures));
        }
        public IKey CreateMatchingKeyFor(ILock @lock)
        {
            var key = new FakeKey();

            foreach (var signature in _signatures)
            {
                key.Signature = signature;

                if (@lock.DoesMatch(key))
                {
                    return key;
                }
            }
            throw new ImpossibleToPickTheLockException(@lock);
        }
        private class FakeKey : IKey
        {
            public string Signature { get; set; }
        }
        public void Pick(ILock @lock)
        {
            var key = new FakeKey();

            foreach (var signature in _signatures)
            {
                key.Signature = signature;
                if (@lock.DoesMatch(key))
                {
                    @lock.UnLock(key);
                    if (!@lock.IsLocked)
                    {
                        return;
                    }
                }
            }
            throw new ImpossibleToPickTheLockException(@lock);
        }
    }
}
