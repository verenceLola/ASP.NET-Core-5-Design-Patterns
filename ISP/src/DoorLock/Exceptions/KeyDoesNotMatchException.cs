using System;
using DoorLock.Interfaces;

namespace DoorLock
{
    public class KeyDoesNotMatchException : Exception
    {
        public KeyDoesNotMatchException(IKey key) : base($"Supplied Key {key} Does Not Match the expected key for the lock") { }
    }
}
