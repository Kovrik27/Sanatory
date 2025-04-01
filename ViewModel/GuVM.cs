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
    public class GuVM : BaseVM
    {
        private ObservableCollection<Guest> guests;

        private MainWindowVM MainVM;

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



            EditGuests = new CommandVM(() =>
            {
                if (SelectedGuest == null)
                    return;
                MainWindowVM.Instance.CurrentPage = new GuAdd(SelectedGuest);
            });

            DeleteGuests = new CommandVM(async() =>
            {
                if (SelectedGuest == null)
                    return;

                if (MessageBox.Show("Выселить гостя?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                   await DB.GetInstance().DeleteGuest(SelectedGuest.ID);
                   Guests.Remove(SelectedGuest);
                    //RoomsRepository.Instance.UpdateStatus2();
                    MainWindowVM.Instance.CurrentPage = new Guests();
                }

            });

            AddProcedure = new CommandVM(() =>
            {
                if (SelectedGuest == null)
                    return;
                MainWindowVM.Instance.CurrentPage = new PrcAddGu(SelectedGuest);
            });

        }

        public async void OnAppearing()
        {
            Guests = await DB.GetInstance().GetAllGuests();
        }
    }
}
