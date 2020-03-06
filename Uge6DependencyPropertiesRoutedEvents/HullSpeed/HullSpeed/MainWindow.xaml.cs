using System;
using System.Collections.Generic;
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

namespace HullSpeed
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Sailboat _sailboat;

        public MainWindow()
        {
            InitializeComponent();

            _sailboat = new Sailboat();

            KeyDown += new KeyEventHandler(MainWindow_KeyDown_FontBig);
            KeyDown += new KeyEventHandler(MainWindow_KeyDown_FontSmall);
        }

        private void MainWindow_KeyDown_FontSmall(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && (Keyboard.IsKeyDown(Key.S)))
            {
                FontSize--;
            }
        }

        private void MainWindow_KeyDown_FontBig(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && (Keyboard.IsKeyDown(Key.L)))
            {
                FontSize++;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ResultLabel.Content = Math.Round(_sailboat.Hullspeed());
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show($"The name of the ship is: {_sailboat.Name}");
        }

        private void ShipName_TextChanged(object sender, TextChangedEventArgs e)
        {
            _sailboat.Name = ShipName.Text;
        }

        private void ShipLength_TextChanged(object sender, TextChangedEventArgs e)
        {
            _sailboat.Length = double.Parse(ShipLength.Text);
        }
    }
}
