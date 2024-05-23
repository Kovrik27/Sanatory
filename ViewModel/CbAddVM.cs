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

        public CommandVM Save { get; set; }
        public CommandVM Add { get; set; }

        private Cabinet cabinets = new();

        public Cabinet Cabinets
        {
            get => cabinets;
            set
            {
                cabinets = value;
                Signal();
            }
        }

        private Staff staff = new();

        public Staff Staff
        {
            get => staff;
            set
            {
                staff = value;
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


                MainWindowVM.Instance.CurrentPage = new Processes();

            });

            

        }


        internal void SetEditCabinets(Cabinet selectedCabinets)
        {
            Cabinets = selectedCabinets;
        }

        internal void SetStaff(Staff selectedStaff)
        {
            Staff = selectedStaff;
        }
    }
}
