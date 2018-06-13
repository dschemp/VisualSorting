using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSorting.Extensions
{
    public static class IEnumerableIntExtension
    {
        public static int GetMax(this IEnumerable<int> vs)
        {
            int max = vs.ElementAt(0);
            foreach (int i in vs)
                max = (i > max) ? i : max;
            return max;
        }
    }
}
