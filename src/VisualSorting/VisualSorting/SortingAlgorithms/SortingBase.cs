using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VisualSorting.SortingAlgorithms
{
    public abstract class SortingBase
    {
        public Tuple<int, int> SelectedItemIndex { get; set; }
        public IEnumerable<int> NumberArray { get; set; }
        public Thread SortThread;

        public event EventHandler NumbersUpdated;

        protected virtual void RaiseNumbersUpdatedEvent()
        {
            NumbersUpdated(this, new EventArgs());
        }

        public abstract void Sort();

        //protected abstract void SortTask();
        //public void Sort()
        //{
        //    SortThread = new Thread(Sort);
        //    SortThread.Start();
        //    // Task.Factory.StartNew(() => SortTask());
        //}

        public SortingBase(IEnumerable<int> numbers)
        {
            this.NumberArray = numbers;
        }

        public void Sleep(int ms = 200)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            while (true)
            {
                //some other processing to do STILL POSSIBLE
                if (stopwatch.ElapsedMilliseconds >= ms)
                {
                    break;
                }
                Thread.Sleep(1); //so processor can rest for a while
            }
        }

        public void SetNumbers(IEnumerable<int> numbers)
        {
            this.NumberArray = numbers;
        }
    }
}
