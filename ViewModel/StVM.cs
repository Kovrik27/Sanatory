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

namespace Sanatory.ViewModel
{
    public class StVM : BaseVM
    {
        private ObservableCollection<Staff> staffs;


        private MainWindowVM MainVM;

        public CommandVM CreateStaff { get; set; }
        public CommandVM EditStaff { get; set; }
        public CommandVM DeleteStaff { get; set; }


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

        public Days SelectedDays
        {
            get => selectedDays;
            set
            {
                selectedDays = value;
                Signal();
            }
        }

        public StVM()
        {
            MainVM = MainWindowVM.Instance;

            string sql = "SELECT s.ID, s.Lastname, s.Name, s.Surname, s.JobTitle, s.Phone, s.Mail, d.ID AS IDD, d.Day AS DayD FROM CrossDaysStaff cds, Staff s, Days d WHERE cds.StaffID = s.ID AND cds.DaysID = d.ID";

            Staffs = new ObservableCollection<Staff>(StaffRepository.Instance.GetAllStaff(sql));
            AllDays = new ObservableCollection<Days> (DaysRepository.Instance.GetDays());
            AllDays.Insert(0, new Days { ID = 0 });
            SelectedDays = AllDays[0];


            CreateStaff = new CommandVM(() =>
            {
                MainVM.CurrentPage = new StAdd(MainVM);
            });

            EditStaff = new CommandVM(() => {
                if (SelectedStaff == null)
                    return;
                MainVM.CurrentPage = new StAdd(MainVM, SelectedStaff);
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
        }

    }
}
