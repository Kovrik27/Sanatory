using Sanatory.Model;
using Sanatory.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sanatory.ViewModel
{
    public class RegAddVM : BaseVM
    {
        MainWindowVM MainVM;
        public CommandVM Save {  get; set; }

        private Room room = new();

        public Room Room
        {
            get => room;
            set
            {
                room = value;
                Signal();
            }
        }
        public RegAddVM() 
        {

            Save = new CommandVM(() =>
            {

                if (Room.ID == 0)
                    RoomsRepository.Instance.AddRoom(Room);
                else
                    RoomsRepository.Instance.UpdateRoom(Room);


                MainVM.CurrentPage = new Registration(MainVM);

            });

        }

        internal void SetMainWindowVM(MainWindowVM MainVM)
        {
            this.MainVM = MainVM;           
        }

        internal void SetEditRoom(Room selectedRoom)
        {
            Room = selectedRoom;
            
        }
    }
}
