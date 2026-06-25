using Sanatory.DTO;
using Sanatory.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для Account.xaml
    /// </summary>
    public partial class Account : Page
    {
        public UserInfo User { get; set; }

        public Account(UserInfo User)
        {
            InitializeComponent();
            this.User = User;
            UserInfo();

        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void Signal([CallerMemberName] string prop = null) =>
          PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        private async void UserInfo()
        {
            if (User != null)
            {
                userLogin.Text = User.Login;
                userLastname.Text = User.Lastname;
                userName.Text = User.Name;
                userSurname.Text = User.Surname;
                //userDataArrival.Text = User.DataArrival;
                //userDataOfDeparture = User.DataOfDeparture;
            }          
            Signal(nameof(User));
        }
    }
}
