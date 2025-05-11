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


        public InformationVM()
        {

        }

        
    }
}
