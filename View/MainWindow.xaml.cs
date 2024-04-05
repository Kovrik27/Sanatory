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
using Sanatory.Model;

namespace Sanatory.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBron(object sender, RoutedEventArgs e)
        {
            Frame.Content = new Registration();
        }

        private void ButtonPersonal(object sender, RoutedEventArgs e)
        {
            Frame.Content = new Staff();
        }

        private void ButtonGosti(object sender, RoutedEventArgs e)
        {
            Frame.Content = new Guests();
        }

        private void ButtonProceduri(object sender, RoutedEventArgs e)
        {
            Frame.Content = new Procedures();
        }

    }
}
