using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VisualSorting.SortingAlgorithms;

namespace VisualSorting.UserInterface
{
    public class UserInterfaceViewModel : ViewModelBase
    {
        public SeriesCollection SeriesCollection { get; set; }

        public List<SortingBase> PossibleSortingAlgorithm { get; set; }

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

            Sort = CreateCommand(_ => MessageBox.Show("Sort-Test"));
            Randomize = CreateCommand(_ => GenerateRandomNumbers());
            RandomizeAsc = CreateCommand(_ => GenerateRandomNumbersAsc());
            RandomizeDesc = CreateCommand(_ => GenerateRandomNumbersDesc());
        }

        private void GenerateRandomNumbersDesc()
        {
            SeriesCollection[0].Values = new ChartValues<int>();
            for (int i = 1; i <= 30; i++)
            {
                SeriesCollection[0].Values.Add(31 - i);
            }
        }

        private void GenerateRandomNumbersAsc()
        {
            SeriesCollection[0].Values = new ChartValues<int>();
            for (int i = 1; i <= 30; i++)
            {
                SeriesCollection[0].Values.Add(i);
            }
        }

        private void GenerateRandomNumbers()
        {
            SeriesCollection[0].Values = new ChartValues<int>();
            for (int i = 0; i < 30; i++)
            {
                SeriesCollection[0].Values.Add(random.Next(1, 51));
            }
        }
    }
}
