using Sanatory.Api;
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
    public class DaysTimAddVM : BaseVM
    {
        public CommandVM Save { get; set; }
        public CommandVM<Events> AddEvent { get; set; }

        private Daytime daytime = new();
        private ObservableCollection<Events> events {  get; set; }

        public Daytime Daytime
        {
            get => daytime;
            set
            {
                daytime = value;
                Signal();
            }
        }

        public ObservableCollection<Events> Events
        {
            get => events;
            set
            {
                events = value;
                Signal();
            }
        }
        public DaysTimAddVM()
        {
            GetAllEvents();
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

        public async void GetAllEvents()
        {
            Events = await DB.GetInstance().GetAllEvents();
        }
      
    }
}

