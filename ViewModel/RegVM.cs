using Sanatory.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.ViewModel
{
    public class RegVM : BaseVM
    {
        private ObservableCollection<Room> rooms;


        public CommandVM CreateRoom {  get; set; }
        public CommandVM EditRoom { get; set;}
        public CommandVM DeleteRoom { get; set;}



        public Room SelectedRoom { get; set; }
        public ObservableCollection<Room> Rooms {
            get => rooms;
            set
            {
                rooms = value;
                Signal();
            }
        }

        public RegVM()
        {
            Rooms = new ObservableCollection<Room>();
        }

    }
}
