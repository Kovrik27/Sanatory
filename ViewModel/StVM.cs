using Sanatory.Api;
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
        private ObservableCollection<JobTitle> jobTitles;


        private MainWindowVM MainVM;

        public CommandVM CreateStaff { get; set; }
        public CommandVM EditStaff { get; set; }
        public CommandVM DeleteStaff { get; set; }

        public CommandVM AddProblem {  get; set; }
        public CommandVM AddCabinet { get; set; }

        public CommandVM DoneProblem { get; set; }
        public CommandVM DoneCabinet { get; set; }

        public Staff SelectedStaff { get; set; }
        private Day selectedDays;
        public ObservableCollection<Day> AllDays { get; set; }

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


        public Day SelectedDays
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
            //Staffs2 = new ObservableCollection<Staff>(StaffRepository.Instance.GetMedStaff(sql2));
            //AllDays.Insert(0, new Days { ID = 0, Day = "Все теги" });
            
            GetAll();


            CreateStaff = new CommandVM(() =>
            {
                MainWindowVM.Instance.CurrentPage = new StAdd();
            });

            EditStaff = new CommandVM(() =>
            {
                if (SelectedStaff == null)
                    return;
                MainWindowVM.Instance.CurrentPage = new StAdd(SelectedStaff);
            });

            DeleteStaff = new CommandVM(async() =>
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
                        await DB.GetInstance().DeleteStaff(SelectedStaff.ID);
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

            DoneProblem = new CommandVM(async() =>
            {
                if (SelectedStaff == null)
                    return;

                if (MessageBox.Show("Сотрудник выполнил задачу?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    await DB.GetInstance().DoneProblem(SelectedStaff.ID);
                    MainWindowVM.Instance.CurrentPage = new Personal();
                }

                });

            DoneCabinet = new CommandVM(async() =>
            {
                if (SelectedStaff == null)
                    return;
                await DB.GetInstance().DoneCabinet(SelectedStaff.ID);
                MainWindowVM.Instance.CurrentPage = new Personal();
            });
        }

        public async void GetAll()
        {
            Staffs = await DB.GetInstance().GetStaffWithProblem();
            Staffs2 = await DB.GetInstance().GetStaffWithCabinet();
            //SelectedDays = AllDays[0];
            AllDays = await DB.GetInstance().GetAllDays();         
        }
    }
}
