using Sanatory.Model;
using Sanatory.View;
using Sanatory.ViewModel;
using System.Windows;

namespace Sanatory.View
{
    public partial class PatientsWindow : Window
    {
        public PatientsWindow(int userId)
        {
            InitializeComponent();

            var vm = (PatientWindowVM)FindResource("ptWinVM");
            LoadGuestAndShowHome(vm, userId);
        }

        private async void LoadGuestAndShowHome(PatientWindowVM vm, int userId)
        {
            await vm.SetGuestId(userId);

            if (vm.Guest != null)
            {
                MainFrame.Content = new PatientHome(vm.Guest.ID);
            }
            else
            {
                MessageBox.Show(
                    $"Не удалось загрузить данные пациента!\n" +
                    $"ID пользователя: {userId}\n" +
                    $"Проверьте, существует ли пациент с этим ID в базе данных.",
                    "Ошибка загрузки",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
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