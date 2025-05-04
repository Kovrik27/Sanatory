using Sanatory.Api;
using Sanatory.Model;
using Sanatory.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.ViewModel
{
    public class ProgVM : BaseVM
    {
        private ObservableCollection<Guest> guests;
        private ObservableCollection<Staff> staffs;
        public ObservableCollection<Guest> Guests
        {
            get => guests;
            set
            {
                guests = value;
                Signal();
            }
        }
        public ObservableCollection<Staff> Staffs
        {
            get => staffs;
            set
            {
                staffs = value;
                Signal();
            }
        }

        public async void GetAll()
        {
            Staffs = await DB.GetInstance().GetAllStaff();
            Guests = await DB.GetInstance().GetAllGuests();
        }
    }
}
