using Sanatory.Model;
using Sanatory.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace Sanatory.ViewModel
{
    public class StAddVM : BaseVM
    {
        public CommandVM Save { get; set; }
        ListBox ListDays;
        public List<Days> AllDays {  get; set; }

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
        public StAddVM()
        {
            AllDays = DaysRepository.Instance.GetDays();


            Save = new CommandVM(() =>
            {
                Staff.Days.Clear();
                foreach (Days days in ListDays.SelectedItems)
                Staff.Days.Add(days);
                                   

                if (Staff.ID == 0)
                    StaffRepository.Instance.AddStaff(Staff);
                else
                    StaffRepository.Instance.UpdateStaff(Staff);


                MainWindowVM.Instance.CurrentPage = new Personal();

            });

        }

        internal void SetEditStaff(Staff selectedStaff)
        {
            Staff = selectedStaff;
            foreach (var days in Staff.Days)
                ListDays.SelectedItems.Add(days);

        }

        internal void SetList(ListBox listDays)
        {
            ListDays = listDays;
        }
    }
}
