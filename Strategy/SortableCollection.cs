using System.Collections.Generic;
using System;

namespace Strategy
{
    public class SortableCollection
    {
        public ISortStrategy SortStrategy { get; set; }
        public IEnumerable<string> Items { get; private set; }
        public SortableCollection(IEnumerable<string> items)
        {
            Items = items;
        }
        public void Sort()
        {
            if (SortStrategy == null)
            {
                throw new NullReferenceException("Sort Strategy not found.");
            }
            Items = SortStrategy.Sort(Items);
        }
    }
}
