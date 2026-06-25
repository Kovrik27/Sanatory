using Microsoft.Extensions.Logging;
using Sanatory.Api;
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
    public class InformationVM : BaseVM
    {
        public ObservableCollection<Events> Eventss
        {
            get => eventss;
            set
            {
                eventss = value;
                Signal();
            }
        }

        public ObservableCollection<Events> eventss { get; set; }


        private DateTime selectedDate;
        private ObservableCollection<Events> events;

        public DateTime SelectedDate
        {
            get => selectedDate;
            set
            {
                if (selectedDate != value)
                {
                    selectedDate = value;
                    Signal();
                    GetEventsDyDate();
                }
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

        public InformationVM()
        {
            GetEventsDyDate();
        }

        private async void GetEventsDyDate()
        {
            if (SelectedDate == null)
            {
                MessageBox.Show("Мероприятия на этот день не найдены!");
            }
            else
            {
                Events = await DB.GetInstance().GetEventsByDate(SelectedDate);
            }
        }


    }
}
