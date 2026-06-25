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
    public class InvAddVM : BaseVM
    {

        public CommandVM Save { get; set; }

        private Resource resource = new();

        public Resource Resource
        {
            get => resource;
            set
            {
                resource = value;
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

        public InvAddVM()
        {

            Save = new CommandVM(async () =>
            {
                if (Resource.Id == 0)
                    await DB.GetInstance().AddNewResource(Resource);
                else
                    await DB.GetInstance().EditResource(Resource);

                MainWindowVM.Instance.CurrentPage = new InventoryPage();

            });



        }


        internal void SetEditResource(Resource resource)
        {
            Resource = resource;
        }
    }
}
