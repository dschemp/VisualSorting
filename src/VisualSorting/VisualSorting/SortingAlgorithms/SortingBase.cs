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

        protected virtual void TriggerNumbersUpdatedEvent()
        {
            NumbersUpdated?.Invoke(this, EventArgs.Empty);
        }

        public abstract void Sort();

        public abstract override string ToString();

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

        public async Task Sleep(int ms = 200)
        {
            //Stopwatch stopwatch = Stopwatch.StartNew();
            //while (true)
            //{
            //    //some other processing to do STILL POSSIBLE
            //    if (stopwatch.ElapsedMilliseconds >= ms)
            //    {
            //        break;
            //    }
            //    Thread.Sleep(1); //so processor can rest for a while
            //}
            await Task.Delay(ms).ConfigureAwait(false);
        }

        public void SetNumbers(IEnumerable<int> numbers)
        {
            this.NumberArray = numbers;
        }
    }
}
