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
    /// Логика взаимодействия для Information.xaml
    /// </summary>
    public partial class Information : Page
    {
        public Information()
        {
            InitializeComponent();
        }

        private void GoBackOnPatientWindow(object sender, RoutedEventArgs e)
        {
            //мы его теряем(закрытие страницы для андрея, он только что сказал про онлифанс страницу) NavigationService.RemoveBackEntry();
            PatientsWindow patientsWindow = new PatientsWindow();
            patientsWindow.Show();
            NavigationService.GoBack();
        }
    }
}
