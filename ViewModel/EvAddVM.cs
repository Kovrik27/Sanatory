using Sanatory.Model;
using Sanatory.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.ViewModel
{
    public class EvAddVM : BaseVM
    {
        public CommandVM Save { get; set; }

        private Events events = new();

        public Events Events
        {
            get => events;
            set
            {
                events = value;
                Signal();
            }
        }
        public EvAddVM()
        {

            Save = new CommandVM(() =>
            {

                if (Events.ID == 0)
                    EventsRepository.Instance.AddEvent(Events);
                else
                    EventsRepository.Instance.UpdateEvent(Events);


                MainWindowVM.Instance.CurrentPage = new Schedule();

            });

        }

       
        internal void SetEditEvent(Events selectedEvent)
        {
            Events = selectedEvent;
        }

    }
}
