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
    public class GuAddVM : BaseVM
    {

        public CommandVM Save { get; set; }

        public CommandVM<Procedure> AddPrc { get; set; }

        private Guest guest = new();
        public Procedure SelectedProcedure { get; set; }
        private ObservableCollection<User> users;
        private ObservableCollection<Procedure> procedures;

        public Guest Guest
        {
            get => guest;
            set
            {
                guest = value;
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


        public ObservableCollection<Procedure> Procedures
        {
            get => procedures;
            set
            {
                procedures = value;
                Signal();
            }
        }

        private string search;

        public string Search
        {
            get => search;
            set
            {
                search = value;
                Signal();
                GetAllProcedures();
            }
        }
        public GuAddVM()
        {
            GetAllProcedures();
            Save = new CommandVM(async () =>
            {

                if (Guest.ID == 0)
                {
                    await DB.GetInstance().AddNewGuest(Guest);
                    await DB.GetInstance().EditStatusRoom(Guest.Room);
                }

                else
                    await DB.GetInstance().EditGuest(Guest);
                MainWindowVM.Instance.CurrentPage = new Guests();

            });


            AddPrc = new CommandVM<Procedure>(async s =>
            {
                if (Guest == null)
                    return;
                await DB.GetInstance().AddProcedureOnGuest(Guest, SelectedProcedure);
                MessageBox.Show("Процедура успешно назначена гостю!", "Юху");
                MainWindowVM.Instance.CurrentPage = new Guests();
            });

        }

        internal void SetEditGuest(Guest selectedGuest)
        {
            Guest = selectedGuest;
        }

        internal void SetRoom(Room? selectedRoom)
        {
            Guest.RoomID = selectedRoom.ID;
            Guest.Room = selectedRoom;
            Signal(nameof(Guest));
        }

        public async void GetAllProcedures()
        {
            var allProcedures = await DB.GetInstance().GetAllProcedure();

            if (!string.IsNullOrEmpty(Search))
            {
                allProcedures = new ObservableCollection<Procedure>(Procedures.Where(s => s.Description.Contains(Search)));
            }

            Procedures = new ObservableCollection<Procedure>(allProcedures);
        }

    }
}
