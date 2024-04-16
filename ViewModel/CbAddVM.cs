using Sanatory.Model;
using Sanatory.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.ViewModel
{
    public class CbAddVM : BaseVM
    {
        MainWindowVM MainVM;
        public CommandVM Save { get; set; }

        private Cabinets cabinets = new();

        public Cabinets Cabinets
        {
            get => cabinets;
            set
            {
                cabinets = value;
                Signal();
            }
        }
        public CbAddVM()
        {

            Save = new CommandVM(() =>
            {

                if (Cabinets.ID == 0)
                    CabinetsRepository.Instance.AddCabinets(Cabinets);
                else
                    CabinetsRepository.Instance.UpdateCabinets(Cabinets);


                MainVM.CurrentPage = new CbAdd(MainVM);

            });

        }

        internal void SetMainWindowVM(MainWindowVM MainVM)
        {
            this.MainVM = MainVM;
        }

        internal void SetEditCabinets(Cabinets selectedCabinets)
        {
            Cabinets = selectedCabinets;

        }
    }
}
