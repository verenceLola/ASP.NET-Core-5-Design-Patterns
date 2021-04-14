using System.Collections.Generic;
using System.Linq;

namespace Strategy
{
    public class SortDescendingStrategy : ISortStrategy
    {
        public IOrderedEnumerable<string> Sort(IEnumerable<string> input) => input.OrderByDescending(x => x);
    }
}
