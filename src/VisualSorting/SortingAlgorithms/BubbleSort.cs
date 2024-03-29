﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisualSorting.UserInterface;

namespace VisualSorting.SortingAlgorithms
{
    public class BubbleSort : SortingBase
    {
        public BubbleSort(IEnumerable<int> numbers) : base(numbers)
        { }

        public override async Task Sort()
        {
            for (int x = NumberArray.Count(); x > 1; x--)
            {
                for (int y = 0; y < NumberArray.Count() - 1; y++)
                {
                    if (NumberArray.ElementAt(y).CompareTo(NumberArray.ElementAt(y + 1)) > 0)
                    {
                        SelectedIndices = new [] { y, y + 1 };
                        NumberArray = Swap.SwapItem(NumberArray, y, y + 1);
                        TriggerNumbersUpdatedEvent();
                    }
                    await Sleep(UserInterfaceViewModel.Delay).ConfigureAwait(false);
                }
            }

            SelectedIndices = null;
        }

        public override string ToString()
        {
            return "Bubblesort";
        }
    }
}