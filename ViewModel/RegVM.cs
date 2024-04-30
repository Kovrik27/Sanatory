using Sanatory.Model;
using Sanatory.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        public CommandVM Broni { get; set; }


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
            MainVM = MainWindowVM.Instance;
            string sql = "SELECT * FROM Rooms";

            Rooms = new ObservableCollection<Room>(RoomsRepository.Instance.GetAllRooms(sql));



            CreateRoom = new CommandVM(() =>
            {
                MainWindowVM.Instance.CurrentPage = new RegAdd();
            });

            EditRoom = new CommandVM(() => {
                if (SelectedRoom == null)
                    return;
                MainWindowVM.Instance.CurrentPage = new RegAdd(SelectedRoom);
            });

            DeleteRoom = new CommandVM(() =>
            {
                if (SelectedRoom == null)
                    return;

                if (MessageBox.Show("Удалить номер?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    RoomsRepository.Instance.Remove(SelectedRoom);
                    Rooms.Remove(SelectedRoom);
                }

            });

            Broni = new CommandVM(() => {
                MainWindowVM.Instance.CurrentPage = new GuAdd(SelectedRoom);

            });

        }


        



    }
}
