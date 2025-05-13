using Sanatory.Api;
using Sanatory.Model;
using Sanatory.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sanatory.ViewModel
{
    public class StaffWindowVM : BaseVM
    {
        public static StaffWindowVM Instance { get; set; }
        private ObservableCollection<Problem> problems;
        private Staff staff;
        public Staff Staff
        {
            get => staff;
            set
            {
                staff = value;
                Signal();
            }
        }


        private Page currentPage;

        public Page CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = value;
                Signal();
            }
        }

        public ObservableCollection<Problem> Problems
        {
            get => problems;
            set
            {
                problems = value;
                Signal();
            }
        }

        public StaffWindowVM()
        {
            Instance = this;
        }


        public async Task SetStaffId(int id)
        {
            Staff = await DB.GetInstance().GetStaffId(id);
            Problems = new ObservableCollection<Problem>( await DB.GetInstance().GetProblemsByStaff(Staff.ID));
        }
    }
}
