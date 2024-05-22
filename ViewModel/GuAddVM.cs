using Sanatory.Model;
using Sanatory.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Sanatory.ViewModel
{
    public class GuAddVM : BaseVM
    {

        public CommandVM Save { get; set; }

        public CommandVM AddPrc { get; set; }

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
                {
                    GuestsRepository.Instance.AddGuest(Guests);
                    RoomsRepository.Instance.UpdateStatus(Guests.Room);
                }
                                   
                else
                    GuestsRepository.Instance.UpdateGuests(Guests);


                MainWindowVM.Instance.CurrentPage = new Guests();

            });


            //AddPrc = new CommandVM<Procedure>(s =>
            //{
            //    GuestsRepository.Instance.AddProcedure(Guests, s);
            //    MainWindowVM.Instance.CurrentPage = new Guests();
            //});

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
