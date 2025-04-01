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
    public class EvAddVM : BaseVM
    {
        public CommandVM Save { get; set; }

        private Events eventt = new();

        public Events Event
        {
            get => eventt;
            set
            {
                eventt = value;
                Signal();
            }
        }
        public EvAddVM()
        {

            Save = new CommandVM(async() =>
            {

                //if (Event.ID == 0)
                //    DB.GetInstance().AddNewEvent(Event);
                //else
                    await DB.GetInstance().EditEvent(Event);


                MainWindowVM.Instance.CurrentPage = new Schedule();

            });

        }

       
        internal void SetEditEvent(Events selectedEvent)
        {
            Event = selectedEvent;
        }

    }
}
