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
    public class AppAddVM : BaseVM
    {

        public CommandVM Save { get; set; }

        private Applications application = new();

        public Applications Application
        {
            get => application;
            set
            {
                application = value;
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

        public AppAddVM()
        {

            Save = new CommandVM(async () =>
            {
                if (Application.Id == 0)
                    await DB.GetInstance().AddNewApplication(Application);
                else
                    await DB.GetInstance().EditApplication(Application);

                MainWindowVM.Instance.CurrentPage = new InventoryPage();

            });



        }


        internal void SetEditApplication(Applications application)
        {
            Application = application;
        }
    }
}
