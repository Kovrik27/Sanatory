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
        private ObservableCollection<Staff> staffs;


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
        public ObservableCollection<Staff> Staffs
        {
            get => staffs;
            set
            {
                staffs = value;
                Signal();
            }
        }


        private string search;
        private bool showCleanUsers;
        public string Search
        {
            get => search;
            set
            {
                search = value;
                Signal();
                GetAllUsers();
            }
        }
        public bool ShowCleanUsers
        {
            get => showCleanUsers;
            set
            {
                showCleanUsers = value;
                Signal();
                GetAllUsers();
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
            var allUsers = await DB.GetInstance().GetAllUsers();

            if (!string.IsNullOrEmpty(Search))
            {
                allUsers = new ObservableCollection<User>(Users.Where(s => s.Login.Contains(Search)));
            }

            if (ShowCleanUsers)
            {
                var staffWithUser = staffs.Select(s => s.UserId).ToHashSet();
                allUsers = new ObservableCollection<User>(Users.Where(s => !staffWithUser.Contains(s.Id)));
            }

            Users = new ObservableCollection<User>(allUsers);
        }
    }
}
