namespace Composite.Interfaces
{
    public interface IComponent
    {
        void Add(IComponent bookComponent);
        void Remove(IComponent bookComponent);
        string Display();
        int Count();
        string Type { get; }
    }
}