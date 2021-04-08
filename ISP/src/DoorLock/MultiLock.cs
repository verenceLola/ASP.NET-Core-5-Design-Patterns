using System;
using DoorLock.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DoorLock
{
    public class MultiLock : ILock
    {
        public readonly List<ILock> _locks;
        public MultiLock(List<ILock> locks)
        {
            _locks = locks ?? throw new ArgumentNullException(nameof(locks));
        }
        public MultiLock(params ILock[] locks) : this(new List<ILock>(locks))
        {
            if (locks == null)
            {
                throw new ArgumentNullException(nameof(locks));
            }
        }
        public bool IsLocked => _locks.Any<ILock>(@lock => @lock.IsLocked);
        public bool DoesMatch(IKey key) => _locks.Any<ILock>(@lock => @lock.DoesMatch(key));
        public void Lock(IKey key)
        {
            if (!DoesMatch(key))
            {
                throw new KeyDoesNotMatchException(key);
            }
            _locks.Where(@lock => @lock.DoesMatch(key)).ToList().ForEach(@lock => @lock.Lock(key));
        }
        public void UnLock(IKey key)
        {
            if (!DoesMatch(key))
            {
                throw new KeyDoesNotMatchException(key);
            }
            _locks.Where(@lock => @lock.DoesMatch(key)).ToList().ForEach(@lock => @lock.UnLock(key));
        }
    }
}
