using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSorting.SortingAlgorithms
{
    public class Swap
    {
        public static IEnumerable<T> SwapItem<T>(IEnumerable<T> array, int indexOne, int indexTwo)
        {
            T[] ts = array.ToArray();

            T temp = ts[indexOne];
            ts[indexOne] = ts[indexTwo];
            ts[indexTwo] = temp;

            return ts;
        }
    }
}
