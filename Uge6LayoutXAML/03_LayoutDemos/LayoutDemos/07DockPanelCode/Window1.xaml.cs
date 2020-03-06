using System.Windows;
using System.Windows.Controls;

namespace _07DockPanelCode
{
   /// <summary>
   /// Interaction logic for Window1.xaml
   /// </summary>
   public partial class Window1 : Window
   {
      public Window1()
      {
         InitializeComponent();
      }

      private void Window_Loaded(object sender, RoutedEventArgs e)
      {
         Button buttonRight = new Button { Content = "Right" };
         DockPanel.SetDock(buttonRight, Dock.Right);
         dockPanel1.Children.Insert(3, buttonRight);  // <-- you may change the place to insert (~ the order they are rendered)
         //dockPanel1.Children.Add(buttonRight);   // <-- this won't work because the button fills the remaining space
      }
   }
}
