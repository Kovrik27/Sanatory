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
    public class CbVM : BaseVM
    {
        private ObservableCollection<Cabinets> cabinets;

        private MainWindowVM MainVM;
        public CommandVM CreateCabinets { get; set; }
        public CommandVM EditCabinets { get; set; }
        public CommandVM DeleteCabinets { get; set; }


        public Cabinets SelectedCabinets { get; set; }
        public ObservableCollection<Cabinets> Cabinets
        {
            get => cabinets;
            set
            {
                cabinets = value;
                Signal();
            }
        }

        public CbVM()
        {
            string sql = "SELECT * FROM Cabinet";

            Cabinets = new ObservableCollection<Cabinets>(CabinetsRepository.Instance.GetAllCabinets(sql));



            CreateCabinets = new CommandVM(() =>
            {
                MainVM.CurrentPage = new CbAdd(MainVM);
            });

            EditCabinets = new CommandVM(() => {
                if (SelectedCabinets == null)
                    return;
                MainVM.CurrentPage = new CbAdd(MainVM, SelectedCabinets);
            });

            DeleteCabinets = new CommandVM(() =>
            {
                if (SelectedCabinets == null)
                    return;

                if (MessageBox.Show("Удалить кабинет?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    CabinetsRepository.Instance.Remove(SelectedCabinets);
                    Cabinets.Remove(SelectedCabinets);
                }

            });



        }


        internal void SetMainWindowVM(MainWindowVM MainVM)
        {
            this.MainVM = MainVM;
        }



    }
}
