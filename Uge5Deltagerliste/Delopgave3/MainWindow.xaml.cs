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

namespace Delopgave3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<string> _myItems;

        public MainWindow()
        {
            InitializeComponent();

            _myItems = new ObservableCollection<string>();
            DeltagerList.ItemsSource = _myItems;

            this.Loaded += new RoutedEventHandler(WindowLoaded);
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            var fs = new FileStream("../../deltagerliste.csv", FileMode.Open);
            var sr = new StreamReader(fs, Encoding.Default);
            
            string line;
            char[] separators = { ';' };
            var lineToAdd = "";

            while ((line = sr.ReadLine()) != null)
            {
                string[] tokens = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                for (var i = 0; i < 3; i++)
                {
                    lineToAdd += tokens[i];
                    lineToAdd += "\t\t";
                }

                _myItems.Add(lineToAdd);

                lineToAdd = "";
            }

            sr.Close();
            fs.Close();
        }
    }
}
