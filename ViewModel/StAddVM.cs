using Sanatory.Api;
using Sanatory.Model;
using Sanatory.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private ListBox _listDays;
        private ObservableCollection<JobTitle> _jobTitles;
        public ObservableCollection<JobTitle> JobTitles
        {
            get => _jobTitles;
            set { _jobTitles = value; Signal(); }
        }

        private ObservableCollection<Day> _allDays;
        public ObservableCollection<Day> AllDays
        {
            get => _allDays;
            set { _allDays = value; Signal(); }
        }

        private Staff _staff = new();
        public Staff Staff
        {
            get => _staff;
            set { _staff = value; Signal(); }
        }

        public StAddVM()
        {
            GetAllDays();

            Save = new CommandVM(async () =>
            {
                if (_listDays == null) return;
                if (Staff.Days == null) Staff.Days = new List<Day>();

                Staff.Days.Clear();
                foreach (Day day in _listDays.SelectedItems)
                    Staff.Days.Add(day);

                if (Staff.ID == 0)
                    await DB.GetInstance().AddNewStaff(Staff);
                else
                    await DB.GetInstance().EditStaff(Staff);

                MainWindowVM.Instance.CurrentPage = new Personal();
            });
        }

        internal void SetEditStaff(Staff selectedStaff)
        {
            Staff = selectedStaff;
            if (_listDays == null || Staff.Days == null) return;

            _listDays.SelectedItems.Clear();
            foreach (var day in Staff.Days)
            {
                if (AllDays?.Contains(day) == true)
                    _listDays.SelectedItems.Add(day);
            }
        }

        internal void SetList(ListBox listDays)
        {
            _listDays = listDays;
        }

        private async void GetAllDays()
        {
            AllDays = await DB.GetInstance().GetAllDays();
            JobTitles = await DB.GetInstance().GetAllJobTitle();
        }
    }
}