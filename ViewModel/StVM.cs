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
        private ObservableCollection<Cabinet> cabinets;
        private ObservableCollection<JobTitle> jobTitles;


        private MainWindowVM MainVM;

        public CommandVM CreateStaff { get; set; }
        public CommandVM EditStaff { get; set; }
        public CommandVM DeleteStaff { get; set; }

        public CommandVM AddProblem {  get; set; }
        public CommandVM AddCabinet { get; set; }

        public CommandVM DoneProblem { get; set; }
        public CommandVM DoneCabinet { get; set; }

        public CommandVM AddProblemOnStaff { get; set; }
        public CommandVM AddCabinetOnStaff { get; set; }

        public Staff SelectedStaff { get; set; }
        private Day selectedDays;
        public Problem SelectedProblem { get; set; }
        public Cabinet SelectedCabinet { get; set; }
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

        public ObservableCollection<Cabinet> Cabinets
        {
            get => cabinets;
            set
            {
                cabinets = value;
                Signal();
            }
        }

        private string search;

        public string Search
        {
            get => search;
            set
            {
                search = value;
                Signal();
                GetAll();
            }
        }

        public StVM()
        {
            MainVM = MainWindowVM.Instance;

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

            AddProblemOnStaff = new CommandVM(async () =>
            {
                if (SelectedStaff == null)
                    return;
                await DB.GetInstance().AddProblemOnStaff(SelectedStaff, SelectedProblem);
                MessageBox.Show("Задача успешно назначена сотруднику!", "Юху");
                MainWindowVM.Instance.CurrentPage = new Personal();
            });

            AddCabinetOnStaff = new CommandVM(async () =>
            {
                if (SelectedStaff == null)
                    return;
                await DB.GetInstance().AddCabinetOnStaff(SelectedStaff, SelectedCabinet);
                MessageBox.Show("Кабинет успешно назначен сотруднику!", "Юху");
                MainWindowVM.Instance.CurrentPage = new Personal();
            });

            DoneProblem = new CommandVM(async() =>
            {
                if (SelectedStaff == null)
                    return;

                if (MessageBox.Show("Сотрудник выполнил задачу?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    await DB.GetInstance().DoneProblem(SelectedProblem.ID);
                    Problems.Remove(SelectedProblem);
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


            var allCabinets = await DB.GetInstance().GetAllCabinets();

            if (!string.IsNullOrEmpty(Search))
            {
                allCabinets = new ObservableCollection<Cabinet>(Cabinets.Where(s => s.Type.Contains(Search)));
            }

            Cabinets = new ObservableCollection<Cabinet>(allCabinets);




            var allProblems = await DB.GetInstance().GetAllProblems();

            if (!string.IsNullOrEmpty(Search))
            {
                allProblems = new ObservableCollection<Problem>(Problems.Where(s => s.Description.Contains(Search)));
            }

            Problems = new ObservableCollection<Problem>(allProblems);
        }

        internal void SetStaff(Staff selectedStaff)
        {
            SelectedStaff = selectedStaff;
        }
    }
}
