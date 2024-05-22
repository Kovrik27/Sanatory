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
        public Guest SelectedGuests { get; set; }
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
            string sql = "SELECT g.ID, g.Surname, g.Name, g.Lastname, g.DataArrival, g.DataOfDeparture, r.Number AS Number, p.Title AS Title FROM Guests g, Rooms r, Procedures p WHERE RoomID = r.ID and r.ID = g.RoomID and p.ID = g.ProcedureID";

            Guests = new ObservableCollection<Guest>(GuestsRepository.Instance.GetAllGuests(sql));



            //CreateGuests = new CommandVM(() =>
            //{
            //    MainWindowVM.Instance.CurrentPage = new GuAdd();
            //});

            EditGuests = new CommandVM(() =>
            {
                if (SelectedGuests == null)
                    return;
                MainWindowVM.Instance.CurrentPage = new GuAdd(SelectedGuests);
            });

            DeleteGuests = new CommandVM(() =>
            {
                if (SelectedGuests == null)
                    return;

                if (MessageBox.Show("Выселить гостя?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    GuestsRepository.Instance.Remove(SelectedGuests);
                    Guests.Remove(SelectedGuests);
                }

            });

            AddProcedure = new CommandVM(() =>
            {
                if (SelectedGuests == null)
                    return;
                MainWindowVM.Instance.CurrentPage = new PrcAddGu(SelectedGuests);
            });



        }

        
    }
}
