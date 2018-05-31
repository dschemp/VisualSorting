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

        public UserInterfaceViewModel()
        {
            PossibleSortingAlgorithm = new List<SortingBase>()
            {
                new BubbleSort(null)
            };

            SeriesCollection = new SeriesCollection()
            {
                new ColumnSeries()
                {
                    Values = new ChartValues<int> { 1 }
                }
            };

            SeriesCollection[0].Values = new ChartValues<int> { 1, 2 };

            Sort = CreateCommand(_ => MessageBox.Show("Sort-Test"));
            Randomize = CreateCommand(_ => MessageBox.Show("Randomize-Test"));
        }
    }
}
