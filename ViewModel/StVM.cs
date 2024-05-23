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
        public CommandVM AddCabinet { get; set; }

        public CommandVM DoneProblem { get; set; }
        public CommandVM DoneCabinet { get; set; }

        public Staff SelectedStaff { get; set; }
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

            string sql = "SELECT s.ID, s.Lastname, s.Name, s.Surname, s.JobTitle, s.Phone, s.Mail, d.ID AS daysID, d.Day AS daysDay, p.Description AS Description FROM Days d JOIN CrossDaysStaff cds ON cds.DaysID = d.ID JOIN Staff s ON cds.StaffID = s.ID LEFT JOIN Problem p ON p.ID = s.ProblemID AND JobTitle NOT LIKE 'Врач%'";
            string sql2 = "SELECT s.ID, s.Lastname, s.Name, s.Surname, s.JobTitle, s.Phone, s.Mail, d.ID AS daysID, d.Day AS daysDay, c.Number AS Number  FROM Days d JOIN CrossDaysStaff cds ON cds.DaysID = d.ID JOIN Staff s ON cds.StaffID = s.ID LEFT JOIN Cabinet c ON c.ID = s.CabinetID WHERE JobTitle LIKE 'Врач%'";
            Staffs = new ObservableCollection<Staff>(StaffRepository.Instance.GetTechStaff(sql));
            Staffs2 = new ObservableCollection<Staff>(StaffRepository.Instance.GetMedStaff(sql2));
            AllDays = new ObservableCollection<Days> (DaysRepository.Instance.GetDays());
            //AllDays.Insert(0, new Days { ID = 0, Day = "Все теги" });
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

                if (SelectedStaff.ProblemID != 0 || SelectedStaff.CabinetID != 0)
                {
                    {
                        MessageBox.Show("Ошибка! Сотрудник не может быть удалён", "Ошибка", MessageBoxButton.OK);
                    }
                }
                else
                {
                    if (MessageBox.Show("Удалить сотрудника?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        StaffRepository.Instance.Remove(SelectedStaff);
                        Staffs.Remove(SelectedStaff);
                        MainWindowVM.Instance.CurrentPage = new Personal();
                    }
                }

            });

            AddProblem = new CommandVM(() =>
            {
                if (SelectedStaff == null)
                    return;
                MainWindowVM.Instance.CurrentPage = new PrAddSt(SelectedStaff);
            });

            AddCabinet = new CommandVM(() =>
            {
                if (SelectedStaff == null)
                    return;
                MainWindowVM.Instance.CurrentPage = new CbAddSt(SelectedStaff);
            });

            DoneProblem = new CommandVM(() =>
            {
                if (SelectedStaff == null)
                    return;

                if (MessageBox.Show("Сотрудник выполнил задачу?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    StaffRepository.Instance.DoneP(SelectedStaff);
                    MainWindowVM.Instance.CurrentPage = new Personal();
                }

            });

            DoneCabinet = new CommandVM(() =>
            {
                if (SelectedStaff == null)
                    return;
                StaffRepository.Instance.DoneC(SelectedStaff);
                MainWindowVM.Instance.CurrentPage = new Personal();
            });
        }

    }
}
