using Sanatory.ViewModel;
using System.Windows;

namespace Sanatory.View
{
    public partial class StaffWindow : Window
    {
        public StaffWindow(int userId)
        {
            InitializeComponent();

            var vm = (StaffWindowVM)FindResource("stWinVM");
            LoadStaffAndShowTasks(vm, userId);
        }

        private async void LoadStaffAndShowTasks(StaffWindowVM vm, int userId)
        {
            await vm.SetStaffId(userId);

            if (vm.Staff != null)
            {
                MainFrame.Content = new StaffTasksPage(vm.Staff.ID);
            }
        }

        private void OutButton(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Close();
        }
    }
}