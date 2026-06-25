using Sanatory.Model;
using Sanatory.ViewModel;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Sanatory.View
{
    /// <summary>
    /// Логика взаимодействия для PatientHome.xaml
    /// </summary>
    public partial class PatientHome : Page
    {
        public PatientHome(int guestId)
        {
            InitializeComponent();
            DataContext = new PatientHomeVM(guestId);
        }

        private void OutButton(object sender, System.Windows.RoutedEventArgs e)
        {
            this.NavigationService.Content = null;
        }
    }
}