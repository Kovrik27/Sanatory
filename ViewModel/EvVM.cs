using Sanatory.Model;
using Sanatory.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sanatory.ViewModel
{
    public class EvVM : BaseVM
    {
        private ObservableCollection<Events> events;

        private MainWindowVM MainVM;
        public CommandVM CreateEvent { get; set; }
        public CommandVM EditEvent { get; set; }
        public CommandVM DeleteEvent { get; set; }


        public Events SelectedEvent { get; set; }
        public ObservableCollection<Events> Events
        {
            get => events;
            set
            {
                events = value;
                Signal();
            }
        }

        public EvVM()
        {
            MainVM = MainWindowVM.Instance;
            string sql = "SELECT * FROM Events";

            Events = new ObservableCollection<Events>(EventsRepository.Instance.GetAllEvents(sql));



            CreateEvent = new CommandVM(() =>
            {
                MainWindowVM.Instance.CurrentPage = new EvAdd();
            });

            EditEvent = new CommandVM(() => {
                if (SelectedEvent == null)
                    return;
                MainWindowVM.Instance.CurrentPage = new EvAdd(SelectedEvent);
            });

            DeleteEvent = new CommandVM(() =>
            {
                if (SelectedEvent == null)
                    return;

                if (MessageBox.Show("Удалить мероприятие?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    EventsRepository.Instance.Remove(SelectedEvent);
                    Events.Remove(SelectedEvent);
                }

            });



        }



    }
}
