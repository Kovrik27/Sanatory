using Sanatory.Api;
using Sanatory.Model;
using Sanatory.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Sanatory.ViewModel
{
    public class StaffWindowVM : BaseVM
    {
        public static StaffWindowVM Instance { get; set; }
        public Problem selectedProblem { get; set; }
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

        public CommandVM CreateProblem { get; set; }
        public CommandVM EditProblem { get; set; }
        public CommandVM EditStatusProblem { get; set; }


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

        public Problem SelectedProblem
        {
            get => selectedProblem;
            set
            {
                selectedProblem = value;
                Signal();
            }
        }

        public StaffWindowVM()
        {
            Instance = this;
          

            EditProblem = new CommandVM(() =>
            {
                if (SelectedProblem == null)
                    return;
                MainWindowVM.Instance.CurrentPage = new PrAdd(SelectedProblem);
            });

            EditStatusProblem = new CommandVM(async () =>
            {
                await DB.GetInstance().DoneProblem(SelectedProblem.ID);
                MessageBox.Show("Задача выполнена!");
            });
        }


        public async Task SetStaffId(int id)
        {
            Staff = await DB.GetInstance().GetStaffId(id);
            Problems = new ObservableCollection<Problem>( await DB.GetInstance().GetProblemsByStaff(Staff.ID));
        }
    }
}
