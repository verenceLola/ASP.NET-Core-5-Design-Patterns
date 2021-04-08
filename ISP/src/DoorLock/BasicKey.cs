using System;
using DoorLock.Interfaces;

namespace DoorLock
{
    public class BasicKey : IKey
    {
        public BasicKey(string signature)
        {
            Signature = signature ?? throw new ArgumentNullException(nameof(signature));
        }
        public string Signature { get; }
    }

}
