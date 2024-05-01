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
        public CommandVM Meropriyatie { get; set; }
        public CommandVM CreateDay { get; set; }
        public Daytime SelectedDaytime {  get; set; }


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
            string sql = "SELECT * FROM Daytime";

            Daytimes = new ObservableCollection<Daytime>(DaystimeRepository.Instance.GetAllDaytime(sql));



            CreateDay = new CommandVM(() =>
            {
                MainWindowVM.Instance.CurrentPage = new DataTimAdd();
            });

            Meropriyatie = new CommandVM(() => {
                MainWindowVM.Instance.CurrentPage = new EvAdd(SelectedDaytime);

            });
        }


    }


}
