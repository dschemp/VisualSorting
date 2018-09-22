using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace VisualSorting.SortingAlgorithms
{
    public abstract class SortingBase
    {
        #region Eigenschaften / Variablen

        public IEnumerable<int> SelectedIndices { get; set; }
        public IEnumerable<int> NumberArray { get; set; }

        public event EventHandler NumbersUpdated;

        public abstract Task Sort();

        public abstract override string ToString();

        #endregion Eigenschaften / Variablen

        public SortingBase(IEnumerable<int> numbers)
        {
            this.NumberArray = numbers;
        }

        protected virtual void TriggerNumbersUpdatedEvent()
        {
            NumbersUpdated?.Invoke(this, EventArgs.Empty);
        }

        public async Task Sleep(int ms = 100)
        {
            await Task.Delay(ms).ConfigureAwait(false);
        }

        public void SetNumbers(IEnumerable<int> numbers)
        {
            this.NumberArray = numbers;
        }
    }
}