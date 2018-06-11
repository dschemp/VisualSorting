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

        public Func<int, int> FuncRandomNumbers;
        public Func<int, int> FuncAscNumbers;
        public Func<int, int> FuncDescNumbers;

        public UserInterfaceViewModel()
        {
            FuncRandomNumbers = x => random.Next(1, 51);
            FuncAscNumbers = x => x;
            FuncDescNumbers = x => 31 - x;

            PossibleSortingAlgorithm = new List<SortingBase>()
            {
                new BubbleSort(null),
                new SelectionSort(null)
            };

            SeriesCollection = new SeriesCollection()
            {
                new ColumnSeries()
            };
            GenerateNumbers(FuncRandomNumbers);

            Sort = CreateCommand(_ => StartSorting());
            Randomize = CreateCommand(_ => GenerateNumbers(FuncRandomNumbers));
            RandomizeAsc = CreateCommand(_ => GenerateNumbers(FuncAscNumbers));
            RandomizeDesc = CreateCommand(_ => GenerateNumbers(FuncDescNumbers));
        }

        private void StartSorting()
        {
            SortingBase sorter = null;

            if (SelectedSorting is BubbleSort)
                sorter = new BubbleSort(Numbers);
            else if (SelectedSorting is SelectionSort)
                sorter = new SelectionSort(Numbers);
            //else if (SelectedSorting is InsertionSort)
            //    sorter = new InsertionSort(Numbers);
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

            selectedSorting.NumbersUpdated += (s, e) => UpdateGraph(selectedSorting.NumberArray);

            selectedSorting.Sort();

            // Set bottom text to end of timer / text "Done."
        }

        private void GenerateNumbers(Func<int, int> func)
        {
            List<int> nums = new List<int>();
            for (int i = 1; i <= 30; i++)
            {
                nums.Add(func(i));
            }
            UpdateGraph(nums);
        }
    }
}
