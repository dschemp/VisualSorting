using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VisualSorting.SortingAlgorithms;

namespace VisualSorting.UserInterface
{
    public class UserInterfaceViewModel : ViewModelBase
    {
        public SeriesCollection SeriesCollection { get; set; }

        public IEnumerable<int> Numbers { get; set; }

        public List<SortingBase> PossibleSortingAlgorithm { get; set; }

        public SortingBase SelectedSorting { get; set; }

        public ICommand Sort { get; set; }
        public ICommand Randomize { get; set; }
        public ICommand RandomizeAsc { get; set; }
        public ICommand RandomizeDesc { get; set; }

        private readonly Random random = new Random();

        public UserInterfaceViewModel()
        {
            PossibleSortingAlgorithm = new List<SortingBase>()
            {
                new BubbleSort(null)
            };

            SeriesCollection = new SeriesCollection()
            {
                new ColumnSeries()
            };
            GenerateRandomNumbers();

            //Sort = CreateCommand(_ => BubbleSort());
            Sort = CreateCommand(_ => StartSorting());
            Randomize = CreateCommand(_ => GenerateRandomNumbers());
            RandomizeAsc = CreateCommand(_ => GenerateNumbersAsc());
            RandomizeDesc = CreateCommand(_ => GenerateNumbersDesc());
        }

        private void StartSorting()
        {
            SortingBase sorter = null;

            if (SelectedSorting is BubbleSort)
                sorter = new BubbleSort(Numbers);
            //else if (SelectedSorting is SelectionSort)
            //    sorter = new SelectionSort(Numbers);
            // ...

            if (sorter != null)
                SortBy(sorter);
        }

        private void UpdateGraph(IEnumerable<int> numbers)
        {
            SeriesCollection[0].Values = new ChartValues<int>();
            foreach (int i in numbers)
                SeriesCollection[0].Values.Add(i);
            this.Numbers = numbers;
        }

        private void SortBy(SortingBase selectedSorting)
        {
            // Set bottom text to timer / text "Sorting..."

            selectedSorting.NumbersUpdated += (s, e) => {
                UpdateGraph(selectedSorting.NumberArray);
            };

            selectedSorting.Sort();

            // Set bottom text to end of timer / text "Done."
        }

        private void GenerateNumbersDesc()
        {
            List<int> nums = new List<int>();
            for (int i = 1; i <= 30; i++)
            {
                nums.Add(31 - i);
            }
            UpdateGraph(nums);
        }

        private void GenerateNumbersAsc()
        {
            List<int> nums = new List<int>();
            for (int i = 1; i <= 30; i++)
            {
                nums.Add(i);
            }
            UpdateGraph(nums);
        }

        private void GenerateRandomNumbers()
        {
            List<int> nums = new List<int>();
            for (int i = 1; i <= 30; i++)
            {
                nums.Add(random.Next(1, 51));
            }
            UpdateGraph(nums);
        }

        protected void BubbleSort()
        {
            for (int x = Numbers.Count(); x > 1; x--)
            {
                for (int y = 0; y < Numbers.Count() - 1; y++)
                {
                    if (Numbers.ElementAt(y).CompareTo(Numbers.ElementAt(y + 1)) > 0)
                    {
                        Numbers = Swap.SwapItem(Numbers, y, y + 1);
                        UpdateGraph(Numbers);
                        Thread.Sleep(100);
                    }
                }
            }
        }

    }
}
