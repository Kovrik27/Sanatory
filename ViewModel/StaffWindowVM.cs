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
        private int staffId;


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


        internal async void SetStaffId(int id)
        {
            staffId = id;
            Problems = await DB.GetInstance().GetProblemsByStaff(id);
        }
    }
}
