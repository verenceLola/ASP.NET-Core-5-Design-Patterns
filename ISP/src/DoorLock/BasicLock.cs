using System;
using DoorLock.Interfaces;

namespace DoorLock
{
    public class BasicLock : ILock
    {
        private readonly string _expectedSignature;
        public BasicLock(string expectedSignature, bool isLocked = false)
        {
            _expectedSignature = expectedSignature ?? throw new ArgumentNullException(nameof(expectedSignature));
            IsLocked = isLocked;
        }
        public bool IsLocked { get; private set; }
        public bool DoesMatch(IKey key) => key.Signature.Equals(_expectedSignature);
        public void Lock(IKey key)
        {
            if (!DoesMatch(key))
            {
                throw new KeyDoesNotMatchException(key);
            }
            IsLocked = true;
        }
        public void UnLock(IKey key)
        {
            if (!DoesMatch(key))
            {
                throw new KeyDoesNotMatchException(key);
            }
            IsLocked = false;
        }
    }

}
