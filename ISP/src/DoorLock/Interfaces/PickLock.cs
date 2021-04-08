namespace DoorLock.Interfaces
{
    public interface IPickLock
    {
        IKey CreateMatchingKeyFor(ILock @lock);
        void Pick(ILock @lock);
    }
}
