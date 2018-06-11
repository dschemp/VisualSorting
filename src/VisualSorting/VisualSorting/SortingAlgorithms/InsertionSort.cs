using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSorting.SortingAlgorithms
{
    public class InsertionSort : SortingBase
    {
        public InsertionSort(IEnumerable<int> numbers) : base(numbers)
        { }

        public override async void Sort()
        {
            for (int x = 1; x < NumberArray.Count(); x++)
            {
                for (int y = x; y > 0; y--)
                {
                    if (NumberArray.ElementAt(y - 1).CompareTo(NumberArray.ElementAt(y)) > 0)
                    {
                        NumberArray = Swap.SwapItem(NumberArray, y, y - 1);
                        TriggerNumbersUpdatedEvent();
                    }
                    await Sleep().ConfigureAwait(false);
                }
            }
        }

        public override string ToString()
        {
            return "Insertionsort";
        }
    }
}
