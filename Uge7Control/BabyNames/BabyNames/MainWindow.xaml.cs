using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BabyNames
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<string> _myTopTenItems;
        private readonly ObservableCollection<string> _mySearchRankItems;
        private readonly List<BabyName> _babyNames;
        private readonly string[,,] _topTenArray;

        public MainWindow()
        {
            InitializeComponent();
            
            _myTopTenItems = new ObservableCollection<string>();
            _mySearchRankItems = new ObservableCollection<string>();
            _babyNames = new List<BabyName>();
            //arrayindex: decade, rank, names
            _topTenArray = new string[11, 10, 2];

            listTop.ItemsSource = _myTopTenItems;
            searchRank.ItemsSource = _mySearchRankItems;

            this.Loaded += new RoutedEventHandler(WindowLoaded);

            listDecades.SelectionChanged += new SelectionChangedEventHandler(listDecades_SelectionChanged);

            searchButton.Click += new RoutedEventHandler(searchButton_Pressed);

            exit.Click += new RoutedEventHandler(exitClicked);
        }

        private void exitClicked(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            var fs = new FileStream("../../babynames.txt", FileMode.Open);
            var sr = new StreamReader(fs, Encoding.Default);

            string line;

            while ((line = sr.ReadLine()) != null)
            {
                Console.WriteLine(line);
                var bab = new BabyName(line);
                _babyNames.Add(bab);
            }


            foreach (var babyName in _babyNames)
            {
                for (var i = 0; i < 11; i++)
                {
                    var rank = babyName.Rank((i*10)+1900);

                    if ((rank <= 10) && (rank > 0)) 
                    {
                        if (string.IsNullOrEmpty(_topTenArray[i, rank-1, 0]))
                        {
                            _topTenArray[i, rank-1, 0] = babyName.Name;
                        }
                        else
                        {
                            _topTenArray[i, rank-1, 1] = babyName.Name;
                        }
                    }
                }
            }
        }

        private void listDecades_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _myTopTenItems.Clear();

            var yearIndex = listDecades.SelectedIndex;

            var toAdd = "";

            for (var i = 0; i < 10; i++)
            {
                toAdd += $"{i + 1}" + " ";
                toAdd += _topTenArray[yearIndex, i, 0];
                toAdd += " and ";
                toAdd += _topTenArray[yearIndex, i, 1];
                toAdd += "\n";
            }

            _myTopTenItems.Add(toAdd);
        }

        private void searchButton_Pressed(object sender, RoutedEventArgs e)
        {
            var nameToSearch = searchBox.Text;

            foreach (var babyName in _babyNames)
            {
                if (babyName.Name.Equals(nameToSearch))
                {
                    avgRankBox.Text = babyName.AverageRank().ToString();

                    var trend = babyName.Trend();
                    if (trend == 0)
                    {
                        trendBox.Text = "Change not consistent";
                    } 
                    else if (trend > 0)
                    {
                        trendBox.Text = "More popular";
                    }
                    else if (trend < 0)
                    {
                        trendBox.Text = "Less popular";
                    }

                    for (var i = 0; i < 11; i++)
                    {
                        var year = (i * 10) + 1900;
                        var rank = babyName.Rank(year);

                        _mySearchRankItems.Add($"{year} {rank}");
                    }

                    break;
                }
            }
        }
    }
}
