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
    public class GuVM : BaseVM
    {
        private ObservableCollection<Guest> guests;

        private MainWindowVM MainVM;


        public CommandVM CreateGuests { get; set; }
        public CommandVM EditGuests { get; set; }
        public CommandVM DeleteGuests { get; set; }
        private Procedure selectedProcedure;
        public CommandVM AddProcedure { get; set; }
        public Guest SelectedGuest { get; set; }
        public ObservableCollection<Guest> Guests
        {
            get => guests;
            set
            {
                guests = value;
                Signal();
            }
        }

        public Procedure SelectedProcedures
        {
            get => selectedProcedure;
            set
            {
                selectedProcedure = value;
                Signal();
            }
        }


        public GuVM()
        {
            MainVM = MainWindowVM.Instance;
            string sql = "SELECT g.ID, g.Surname, g.Name, g.Lastname, g.Pasport, g.Policy, g.DataArrival, g.DataOfDeparture, r.Number AS Number, p.Title AS Title FROM Guests g JOIN Rooms r, Procedures p WHERE g.RoomID = r.ID  AND  g.ProcedureID = p.ID;";

            Guests = new ObservableCollection<Guest>(GuestsRepository.Instance.GetAllGuests(sql));


            EditGuests = new CommandVM(() =>
            {
                if (SelectedGuest == null)
                    return;
                MainWindowVM.Instance.CurrentPage = new GuAdd(SelectedGuest);
            });

            DeleteGuests = new CommandVM(() =>
            {
                if (SelectedGuest == null)
                    return;

                if (MessageBox.Show("Выселить гостя?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    GuestsRepository.Instance.DoneG(SelectedGuest);
                    //RoomsRepository.Instance.UpdateStatus2();
                    MainWindowVM.Instance.CurrentPage = new Guests();
                 
                    //Guests.Remove(SelectedGuests);
                }

            });

            AddProcedure = new CommandVM(() =>
            {
                if (SelectedGuest == null)
                    return;
                MainWindowVM.Instance.CurrentPage = new PrcAddGu(SelectedGuest);
            });



        }

        
    }
}
