using System;
using DoorLock.Interfaces;

namespace DoorLock.Exceptions
{
    public class ImpossibleToPickTheLockException : Exception
    {
        public ImpossibleToPickTheLockException(ILock @lock) : base($"Unable to pick the lock {@lock}") { }
    }
}
