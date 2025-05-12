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

        private string search;

        public string Search
        {
            get => search;
            set
            {
                search = value;
                Signal();
                GetAllGuests();
            }
        }

        public GuVM()
        {
            MainVM = MainWindowVM.Instance;
            GetAllGuests();

            EditGuests = new CommandVM(() =>
            {
                if (SelectedGuest == null)
                    return;
                MainWindowVM.Instance.CurrentPage = new GuAdd(SelectedGuest);
            });


            AddProcedure = new CommandVM(() =>
            {
                if (SelectedGuest == null)
                    return;
                MainWindowVM.Instance.CurrentPage = new PrcAddGu(SelectedGuest);
            });

        }

        private async void GetAllGuests()
        {
            var allGuests = await DB.GetInstance().GetAllGuests();

            if (!string.IsNullOrEmpty(Search))
            {
                allGuests = new ObservableCollection<Guest>(Guests.Where(s => s.Lastname.Contains(Search)));
            }

            Guests = new ObservableCollection<Guest>(allGuests);
        }

        
    }
}
