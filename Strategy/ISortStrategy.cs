using System.Collections.Generic;
using System.Linq;

namespace Strategy
{
    public interface ISortStrategy
    {
        IOrderedEnumerable<string> Sort(IEnumerable<string> input);
    }
}
