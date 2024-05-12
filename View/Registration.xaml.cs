using Sanatory.Model;
using Sanatory.ViewModel;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
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
        public Registration()
        {
            InitializeComponent();           
        }

        private void ButtonVis (object sender, SelectionChangedEventArgs e)
        {
            if (Rm.SelectedItem != null)
            {
                var vm = DataContext as RegVM;
                if (vm.SelectedRoom.Status == "Свободен")
                    Bronirovanie.Visibility = Visibility.Visible;
                else
                    Visilenie.Visibility = Visibility.Visible;
            }

        }

        //private void Broni(object sender, RoutedEventArgs e)
        //{

        //    MainWindowVM.Instance.CurrentPage = new GuAdd();
        //}

        //private void Viselit(object sender, RoutedEventArgs e)
        //{
        //    MessageBox.Show("Гость выселен");
        //}
    }
}
