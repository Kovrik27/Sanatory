using Sanatory.Api;
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
            Save = new CommandVM(async() =>
            {

                if (Room.ID == 0)
                {
                    await DB.GetInstance().AddNewRoom(Room);
                }

                else
                   await DB.GetInstance().EditRoom(Room);

                MainWindowVM.Instance.CurrentPage = new Registration();

            });
        }


        internal void SetEditRoom(Room selectedRoom)
        {
            Room = selectedRoom;
            
        }
    }
}
