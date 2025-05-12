using Sanatory.Api;
using Sanatory.Model;
using Sanatory.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sanatory.ViewModel
{
    public class ProgVM : BaseVM
    {
        private ObservableCollection<Guest> guests;
        private ObservableCollection<Staff> staffs;
        private ObservableCollection<User> users;
        public ObservableCollection<Guest> Guests
        {
            get => guests;
            set
            {
                guests = value;
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

        public ObservableCollection<User> Users
        {
            get => users;
            set
            {
                users = value;
                Signal();
            }
        }

        private Staff SelectedStaff { get; set; }
        private Guest SelectedGuets {  get; set; }
        private User SelectedUser { get; set; }

        public CommandVM UsersList { get; set; }
        public CommandVM AddUserOn {  get; set; }

        private string search;

        public string Search
        {
            get => search;
            set
            {
                search = value;
                Signal();
                GetAll();
            }
        }
        private void OpenUsersList()
        {
            CurrentPage = new UsersList();
        }

        public ProgVM()
        {
            GetAll();

            Instance = this;

            UsersList = new CommandVM(() =>
            {
                OpenUsersList();
            });

            AddUserOn = new CommandVM(async () =>
            {
                await DB.GetInstance().AddUserOn(SelectedStaff, SelectedGuets, SelectedUser);
            });

        }
        public async void GetAll()
        {
            Staffs = await DB.GetInstance().GetAllStaff();
            Guests = await DB.GetInstance().GetAllGuests();

            var allUsers = await DB.GetInstance().GetAllUsers();

            if (!string.IsNullOrEmpty(Search))
            {
                allUsers = new ObservableCollection<User>(Users.Where(s => s.Login.Contains(Search)));
            }

            Users = new ObservableCollection<Room>(allUsers);
        }

        public static ProgVM Instance { get; private set; }

        private Page currentPage;

        public Page CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = value;
                Signal();
            }
        }


    }
}

