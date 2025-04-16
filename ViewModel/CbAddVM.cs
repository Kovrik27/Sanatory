using Sanatory.Api;
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

        private Cabinet cabinet = new();

        public Cabinet Cabinet
        {
            get => cabinet;
            set
            {
                cabinet = value;
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

            Save = new CommandVM(async() =>
            {
                if (Cabinet.ID == 0)
                    await DB.GetInstance().AddNewCabinet(Cabinet);
                else
                    await DB.GetInstance().EditCabinet(Cabinet);

                MainWindowVM.Instance.CurrentPage = new Processes();
                
            });



        }


        internal void SetEditCabinet(Cabinet selectedCabinet)
        {
            Cabinet = selectedCabinet;
        }

        internal void SetStaff(Staff selectedStaff)
        {
            Staff = selectedStaff;
        }
    }
}
