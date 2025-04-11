using Sanatory.Api;
using Sanatory.Model;
using Sanatory.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.ViewModel
{
    public class DaysTimAddVM : BaseVM
    {
        public CommandVM Save { get; set; }
        public CommandVM<Events> AddEvent { get; set; }

        private Daytime daytime = new();

        public Daytime Daytime
        {
            get => daytime;
            set
            {
                daytime = value;
                Signal();
            }
        }
        public DaysTimAddVM()
        {

            Save = new CommandVM(async() =>
            {

                if (Daytime.ID == 0)
                    await DB.GetInstance().AddNewDaytime(Daytime);
                else
                    await DB.GetInstance().EditDaytime(Daytime);

                MainWindowVM.Instance.CurrentPage = new Schedule();

            });

            //AddEvent = new CommandVM<Events>(s =>
            //{
            //    DB.GetInstance().AddNewEvent(Daytime, s);
            //    MainWindowVM.Instance.CurrentPage = new Schedule();
            //});



        }


        internal void SetEditDaytime(Daytime selectedDayTime)
        {
            Daytime = selectedDayTime;

        }

      
    }
}

