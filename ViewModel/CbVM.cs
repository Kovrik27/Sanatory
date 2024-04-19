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
        private ObservableCollection<Cabinet> cabinets;

        private MainWindowVM MainVM;
        public CommandVM CreateCabinets { get; set; }
        public CommandVM EditCabinets { get; set; }
        public CommandVM DeleteCabinets { get; set; }


        public Cabinet SelectedCabinets { get; set; }
        public ObservableCollection<Cabinet> Cabinets
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
            MainVM = MainWindowVM.Instance;
            string sql = "SELECT * FROM Cabinet";

            Cabinets = new ObservableCollection<Cabinet>(CabinetsRepository.Instance.GetAllCabinets(sql));



            CreateCabinets = new CommandVM(() =>
            {
                MainWindowVM.Instance.CurrentPage = new CbAdd();
            });

            EditCabinets = new CommandVM(() => {
                if (SelectedCabinets == null)
                    return;
                MainWindowVM.Instance.CurrentPage = new CbAdd(SelectedCabinets);
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





    }
}
