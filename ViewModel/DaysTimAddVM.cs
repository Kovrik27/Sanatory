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
        private Daytime selectedDaytime;
        private Events selectedEvent;
        private ObservableCollection<Events> events;
        private string search;

        public Daytime Daytime
        {
            get => daytime;
            set
            {
                daytime = value;
                Signal();
            }
        }

        public Daytime SelectedDaytime
        {
            get => selectedDaytime;
            set
            {
                selectedDaytime = value;
                Signal();
            }
        }

        public Events SelectedEvent
        {
            get => selectedEvent;
            set
            {
                selectedEvent = value;
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

        public string Search
        {
            get => search;
            set
            {
                search = value;
                Signal();
                FilterEvents();
            }
        }

        public DaysTimAddVM()
        {
            Daytime = new Daytime();

            GetAllEvents();

            Save = new CommandVM(async () =>
            {
                if (Daytime == null)
                    Daytime = new Daytime();

                if (Daytime.Id == 0)
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
            if (selectedDayTime != null)
            {
                Daytime = selectedDayTime;
                SelectedDaytime = selectedDayTime;
            }
        }

        public async void GetAllEvents()
        {
            var allEvents = await DB.GetInstance().GetAllEvents();
            Events = new ObservableCollection<Events>(allEvents ?? Enumerable.Empty<Events>());
        }

        private void FilterEvents()
        {
            if (Events == null)
                return;

        }
    }
}