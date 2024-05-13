using Sanatory.Model;
using Sanatory.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Shapes;

namespace Sanatory.ViewModel
{
    public class StVM : BaseVM
    {
        private ObservableCollection<Staff> staffs;
        private ObservableCollection<Staff> staffs2;
        private ObservableCollection<Problem> problems;


        private MainWindowVM MainVM;

        public CommandVM CreateStaff { get; set; }
        public CommandVM EditStaff { get; set; }
        public CommandVM DeleteStaff { get; set; }

        public CommandVM AddProblem {  get; set; }
        public CommandVM ProblemDone { get; set; }

        public Staff SelectedStaff { get; set; }
        public Problem SelectedProblem { get; set; }
        private Days selectedDays;
        public ObservableCollection<Days> AllDays { get; set; }

        public ObservableCollection<Staff> Staffs
        {
            get => staffs;
            set
            {
                staffs = value;
                Signal();
            }
        }

        public ObservableCollection<Staff> Staffs2
        {
            get => staffs2;
            set
            {
                staffs2 = value;
                Signal();
            }
        }


        public Days SelectedDays
        {
            get => selectedDays;
            set
            {
                selectedDays = value;
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

        public StVM()
        {
            MainVM = MainWindowVM.Instance;

            string sql = "SELECT s.ID, s.Lastname, s.Name, s.Surname, s.JobTitle, s.Phone, s.Mail, d.ID AS daysID, d.Day AS daysDay, p.Description AS Description FROM CrossDaysStaff cds, Staff s, Days d, Problem p WHERE cds.StaffID = s.ID AND cds.DaysID = d.ID AND p.ID = ProblemID AND JobTitle NOT LIKE 'Врач%'";
            string sql2 = "SELECT s.ID, s.Lastname, s.Name, s.Surname, s.JobTitle, s.Phone, s.Mail, d.ID AS daysID, d.Day AS daysDay FROM CrossDaysStaff cds, Staff s, Days d WHERE cds.StaffID = s.ID AND cds.DaysID = d.ID AND JobTitle LIKE 'Врач%'";
            Staffs = new ObservableCollection<Staff>(StaffRepository.Instance.GetAllStaff(sql));
            Staffs2 = new ObservableCollection<Staff>(StaffRepository.Instance.GetAllStaff(sql2));
            AllDays = new ObservableCollection<Days> (DaysRepository.Instance.GetDays());
            Problems = new ObservableCollection<Problem>(ProblemRepository.Instance.GetAllProblem(sql));
            AllDays.Insert(0, new Days { ID = 0, Day = "Все теги" });
            SelectedDays = AllDays[0];


            CreateStaff = new CommandVM(() =>
            {
                MainWindowVM.Instance.CurrentPage = new StAdd();
            });

            EditStaff = new CommandVM(() => {
                if (SelectedStaff == null)
                    return;
                MainWindowVM.Instance.CurrentPage = new StAdd(SelectedStaff);
            });

            DeleteStaff = new CommandVM(() =>
            {
                if (SelectedStaff == null)
                    return;

                if (MessageBox.Show("Удалить сотрудника?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    StaffRepository.Instance.Remove(SelectedStaff);
                    Staffs.Remove(SelectedStaff);
                }

            });

            AddProblem = new CommandVM(() =>
            {
                if (SelectedStaff == null)
                    return;
                MainWindowVM.Instance.CurrentPage = new PrAddSt(SelectedStaff);
            });
       

            ProblemDone = new CommandVM(() =>
            {
                if (SelectedStaff  == null) 
                    return;


                if (MessageBox.Show("Задача выполнена?", "Молодец", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    //ProblemRepository.Instance.RemoveFromStaff(SelectedProblem);
                    Problems.Remove(SelectedProblem);
                }
            });
        }

    }
}
