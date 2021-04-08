namespace DoorLock.Interfaces
{
    public interface ILock
    {
        bool IsLocked { get; }
        void Lock(IKey key);
        void UnLock(IKey key);
        bool DoesMatch(IKey key);
    }
}
