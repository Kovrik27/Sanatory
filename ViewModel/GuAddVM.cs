using Sanatory.Model;
using Sanatory.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.ViewModel
{
    public class GuAddVM : BaseVM
    {

        public CommandVM Save { get; set; }

        private Guest guests = new();

        public Guest Guests
        {
            get => guests;
            set
            {
                guests = value;
                Signal();
            }
        }
        public GuAddVM()
        {

            Save = new CommandVM(() =>
            {

                if (Guests.ID == 0)
                    GuestsRepository.Instance.AddGuest(Guests);
                else
                    GuestsRepository.Instance.UpdateGuest(Guests);


                MainWindowVM.Instance.CurrentPage = new Guests();

            });

        }


        internal void SetEditGuest(Guest selectedGuest)
        {
            Guests = selectedGuest;

        }

        internal void SetRoom(Room? selectedRoom)
        {
            Guests.RoomID = selectedRoom.ID;
            Guests.Room = selectedRoom;
            Signal(nameof(Guests));
        }
    }
}
