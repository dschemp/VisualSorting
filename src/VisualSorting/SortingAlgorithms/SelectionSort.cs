using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisualSorting.UserInterface;

namespace VisualSorting.SortingAlgorithms
{
    public class SelectionSort : SortingBase
    {
        public SelectionSort(IEnumerable<int> numbers) : base(numbers)
        { }

        public override async Task Sort()
        {
            for (int i = 0; i < NumberArray.Count() - 1; i++)
            {
                int arrayNum = i;
                for (int j = i + 1; j < NumberArray.Count(); j++)
                {
                    SelectedIndices = new[] {j, i};
                    if (NumberArray.ElementAt(j).CompareTo(NumberArray.ElementAt(i)) < 0 && NumberArray.ElementAt(arrayNum).CompareTo(NumberArray.ElementAt(j)) > 0)
                    {
                        arrayNum = j;
                    }
                    await Sleep(UserInterfaceViewModel.Delay).ConfigureAwait(false);
                }
                // Swap
                SelectedIndices = new[] {i, arrayNum};
                NumberArray = Swap.SwapItem(NumberArray, i, arrayNum);
                TriggerNumbersUpdatedEvent();
            }

            SelectedIndices = null;
        }

        public override string ToString()
        {
            return "Selectionsort";
        }
    }
}