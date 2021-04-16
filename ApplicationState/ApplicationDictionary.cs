using ApplicationState.Interfaces;
using System.Collections.Generic;


namespace ApplicationState
{
    public class ApplicationDictionary : IApplicationState
    {
        private readonly Dictionary<string, object> _memoeryCache = new Dictionary<string, object>();
        public TItem Get<TItem>(string key)
        {
            if (!Has<TItem>(key))
            {
                return default;
            }
            return (TItem)_memoeryCache[key];
        }
        public bool Has<TItem>(string key) => _memoeryCache.ContainsKey(key) && _memoeryCache[key] is TItem;
        public void Set<TItem>(string key, TItem value) => _memoeryCache[key] = value;
    }
}
