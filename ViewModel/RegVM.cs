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
    public class RegVM : BaseVM
    {
        private ObservableCollection<Room> rooms;

        private MainWindowVM MainVM;
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
            //Rooms = new ObservableCollection<Room>(RoomsRepository.Instance.GetAllRooms(sql));



            CreateRoom = new CommandVM(() =>
            {
                MainVM.CurrentPage = new RegAdd(MainVM);
            });

            EditRoom = new CommandVM(() => {
                if (SelectedRoom == null)
                    return;
                MainVM.CurrentPage = new RegAdd(MainVM, SelectedRoom);
            });

            //DeleteRoom = new CommandVM(() => {
            //    if (SelectedRoom == null)
            //        return;

            //    if (MessageBox.Show("Удалить номер?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            //    {
            //        RoomsRepository.Instance.Remove(SelectedRoom);
            //        Rooms.Remove(SelectedRoom);
            //    }
            //});



        }
        internal void SetMainWindowVM(MainWindowVM MainVM)
        {
            this.MainVM = MainVM;
        }



    }
}
