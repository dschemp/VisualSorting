using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSorting.SortingAlgorithms
{
    public class BubbleSort : SortingBase
    {
        public BubbleSort(IEnumerable<int> numbers) : base(numbers)
        { }

        protected override void SortTask()
        {
            for (int x = NumberArray.Count(); x > 1; x--)
            {
                for (int y = 0; y < NumberArray.Count() - 1; y++)
                {
                    if (NumberArray.ElementAt(y).CompareTo(NumberArray.ElementAt(y + 1)) > 0)
                    {
                        SelectedItemIndex = new Tuple<int, int>(y, y + 1);
                        NumberArray = Swap.SwapItem(NumberArray, y, y + 1);
                        RaiseNumbersUpdatedEvent();
                        //Sleep(50);
                    }
                }
            }
        }
    }
}
