using System.Linq;
using System.Collections.Generic;

namespace Strategy
{
    public class SortAscendingStrategy : ISortStrategy
    {
        public IOrderedEnumerable<string> Sort(IEnumerable<string> input) => input.OrderBy(x => x);
    }
}
