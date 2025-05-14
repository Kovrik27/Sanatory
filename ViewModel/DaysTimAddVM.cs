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

        private Daytime daytime;

        public Daytime SelectedDaytime;

        public Events SelectedEvent;

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

        private string search;

        public string Search
        {
            get => search;
            set
            {
                search = value;
                Signal();
                GetAllEvents();
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

            AddEvent = new CommandVM<Events>(s =>
            {
                DB.GetInstance().AddNewEventOnDay(SelectedDaytime, SelectedEvent);
                MainWindowVM.Instance.CurrentPage = new Schedule();
            });



        }


        internal void SetEditDaytime(Daytime selectedDayTime)
        {
            Daytime = selectedDayTime;

        }

        public async void GetAllEvents()
        {
            var allEvents = await DB.GetInstance().GetAllEvents();

            if (!string.IsNullOrEmpty(Search))
            {
                allEvents = new ObservableCollection<Events>(Events.Where(s => s.Title.Contains(Search)));
            }

            Events = new ObservableCollection<Events>(allEvents);
        }
      
    }
}

