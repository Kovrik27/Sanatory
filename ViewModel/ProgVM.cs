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
using System.Windows.Controls;

namespace Sanatory.ViewModel
{
    public class ProgVM : BaseVM
    {
        private ObservableCollection<Guest> guests;
        private ObservableCollection<Staff> staffs;
        private ObservableCollection<Staff> staffs2;
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

        public ObservableCollection<Staff> Staffs2
        {
            get => staffs2;
            set
            {
                staffs2 = value;
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

        public Staff SelectedStaff { get; set; }
        public Guest SelectedGuets {  get; set; }
        public User SelectedUser { get; set; }

        public CommandVM UsersList { get; set; }
        public CommandVM AddUserOnStaffPage {  get; set; }
        public CommandVM AddUserOnGuestPage { get; set; }
        public CommandVM AddUserOnStaff { get; set; }
        public CommandVM AddUserOnGuest { get; set; }


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

            AddUserOnStaffPage = new CommandVM(() =>
            {
                AddUserOn addUserOn = new AddUserOn(SelectedStaff);
                addUserOn.Show();
            });

            AddUserOnGuestPage = new CommandVM(() =>
            {
                AddUserOn addUserOn = new AddUserOn(SelectedGuets);
                addUserOn.Show();
            });

            AddUserOnStaff = new CommandVM(async () =>
            {
                await DB.GetInstance().AddUserOnStaff(SelectedStaff, SelectedUser);
                MessageBox.Show("Юзер успешно добавлен  сотруднику!");
            });

            AddUserOnGuest = new CommandVM(async () =>
            {
                await DB.GetInstance().AddUserOnGuest(SelectedGuets, SelectedUser);
                MessageBox.Show("Юзер успешно добавлен  гостю!");
            });

        }
        public async void GetAll()
        {
            Staffs = await DB.GetInstance().GetStaffWithProblem();
            Staffs2 = await DB.GetInstance().GetStaffWithCabinet();
            Guests = await DB.GetInstance().GetAllGuests();

            var allUsers = await DB.GetInstance().GetAllUsers();

            if (!string.IsNullOrEmpty(Search))
            {
                allUsers = new ObservableCollection<User>(Users.Where(s => s.Login.Contains(Search)));
            }

            Users = new ObservableCollection<User>(allUsers);
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

        internal void SetStaff(Staff selectedStaff)
        {
            SelectedStaff = selectedStaff;
        }
        internal void SetGuest(Guest selectedGuest)
        {
            SelectedGuets = selectedGuest;
        }
    }
}

