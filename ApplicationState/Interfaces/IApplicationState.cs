namespace ApplicationState.Interfaces
{
    public interface IApplicationState
    {
        TItem Get<TItem>(string key);
        bool Has<TItem>(string key);
        void Set<TItem>(string key, TItem value);
    }
}
