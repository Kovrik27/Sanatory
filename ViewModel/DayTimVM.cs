using Sanatory.Model;
using Sanatory.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.ViewModel
{
    public class DayTimVM : BaseVM
    {
        public ObservableCollection<Daytime> daytimes;
        private MainWindowVM MainVM;
        public CommandVM CreateDay { get; set; }
        public Daytime SelectedDaytime {  get; set; }
        public CommandVM AddEvent { get; set; }


        public ObservableCollection<Daytime> Daytimes
        {
            get => daytimes;
            set
            {
                daytimes = value;
                Signal();
            }
        }

        public DayTimVM()
        {
            MainVM = MainWindowVM.Instance;
            string sql = "SELECT d.Time, e.ID, e.Title, e.Time, e.Place FROM Daytime d, Events e WHERE EventID = e.ID";

            Daytimes = new ObservableCollection<Daytime>(DaystimeRepository.Instance.GetAllDaytime(sql));



            CreateDay = new CommandVM(() =>
            {
                MainWindowVM.Instance.CurrentPage = new DataTimAdd();
            });

            AddEvent = new CommandVM(() =>
            {
                if (SelectedDaytime == null)
                    return;
                MainWindowVM.Instance.CurrentPage = new EvAdd();
            });
        }


    }


}
