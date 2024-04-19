using Sanatory.Model;
using Sanatory.ViewModel;
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

namespace Sanatory.View
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        public Registration(ViewModel.MainWindowVM MainVM)
        {
            InitializeComponent();           

            var vm = DataContext as RegVM;
            vm?.SetMainWindowVM(MainVM);
        }

        private void ButtonVis (object sender, SelectionChangedEventArgs e)
        {
            if (Rm.SelectedItem != null)            
                Bronirovanie.Visibility = Visibility.Visible;           
            else            
                Bronirovanie.Visibility = Visibility.Hidden;
            
        }

        private void Broni(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Гость заселён");
        }
    }
}
