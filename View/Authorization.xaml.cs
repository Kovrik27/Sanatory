using Sanatory.Api;
using Sanatory.Model;
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
using System.Windows.Shapes;

namespace Sanatory.View
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
            DataContext = this;

        }



        private void AuthorizationButton(object sender, RoutedEventArgs e)
        {
            AddNewUser addNewUser = new AddNewUser();
            addNewUser.ShowDialog();
        }

        private async void PathButton(object sender, RoutedEventArgs e)
        {
            string username = UserTextBox.Text;
            string password = PasswordTextBox.Password;

            User user = new User { Login = username, Password = password, Role = new Role{ Title = "Администратор"  } };
            User result = await DB.GetInstance().CheckUser(user);

            switch (result.RoleId)
            {
                case 1:
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    Close();
                    break;

                case 3:
                    StaffWindow staffWindow = new StaffWindow(result.Id);
                    staffWindow.Show();
                    Close();
                    break;

                case 7:
                    StaffWindow staffWindow2 = new StaffWindow(result.Id);
                    staffWindow2.Show();
                    Close();
                    break;

                case 5:
                    PatientsWindow patientsWindow = new PatientsWindow(result.Id);
                    patientsWindow.Show();
                    Close();
                    break;

                case 2:
                    ProgWindow progWindow = new ProgWindow();
                    progWindow.Show();
                    Close();
                    break;
                default:
                    MessageBox.Show("Неизвестный тип пользователя.");
                    break;

            }

            
        }
    }
}
