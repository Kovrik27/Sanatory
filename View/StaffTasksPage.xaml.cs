using Sanatory.ViewModel;
using System.Windows.Controls;

namespace Sanatory.View
{
    public partial class StaffTasksPage : Page
    {
        public StaffTasksPage(int staffId)
        {
            InitializeComponent();
            DataContext = new StaffTasksPageVM(staffId);
        }

        private void OutButton(object sender, System.Windows.RoutedEventArgs e)
        {
            this.NavigationService.Content = null;
        }
    }
}