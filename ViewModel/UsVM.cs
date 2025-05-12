using Sanatory.Api;
using Sanatory.Model;
using Sanatory.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sanatory.ViewModel
{
    public class UsVM : BaseVM
    {
        private ObservableCollection<User> users;

        private MainWindowVM MainVM;
        public CommandVM CreateUser { get; set; }
        public CommandVM EditUser { get; set; }
        public CommandVM DeleteUser { get; set; }
        public CommandVM CheckUser { get; set; }

        public User SelectedUser { get; set; }
        public ObservableCollection<User> Users
        {
            get => users;
            set
            {
                users = value;
                Signal();
            }
        }

        public UsVM()
        {
            MainVM = MainWindowVM.Instance;
            GetAllUsers();

            CreateUser = new CommandVM(() =>
            {
                MainWindowVM.Instance.CurrentPage = new UsAdd();
            });

            EditUser = new CommandVM(() =>
            {
                if (SelectedUser == null)
                    return;
                MainWindowVM.Instance.CurrentPage = new UsAdd(SelectedUser);
            });

            DeleteUser = new CommandVM(async () =>
            {
                if (SelectedUser == null)
                    return;

                if (MessageBox.Show("Удалить юзера?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    await DB.GetInstance().DeleteUser(SelectedUser.Id);
                    Users.Remove(SelectedUser);
                    MainWindowVM.Instance.CurrentPage = new UsersList();
                }
            });

        }

        public async void GetAllUsers()
        {
            Users = await DB.GetInstance().GetAllUsers();
        }
    }
}
