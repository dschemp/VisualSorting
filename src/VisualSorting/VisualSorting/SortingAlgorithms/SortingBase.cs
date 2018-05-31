using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VisualSorting.SortingAlgorithms
{
    public abstract class SortingBase
    {
        public int SelectedItemIndex { get; set; }
        public IEnumerable<int> NumberArray { get; set; }

        public abstract void Sort();
        public SortingBase(IEnumerable<int> numbers)
        {
            this.NumberArray = numbers;
        }

        public void Sleep(int ms)
        {
            Thread.Sleep(ms);
        }

        public void SetNumbers(IEnumerable<int> numbers)
        {
            this.NumberArray = numbers;
        }
    }
}
