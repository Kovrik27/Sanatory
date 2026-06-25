using Sanatory.Api;
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
        public CommandVM CreateRoom {  get; set; }
        public CommandVM EditRoom { get; set;}
        public CommandVM DeleteRoom { get; set;}
        public CommandVM Broni { get; set; }
        public CommandVM Visilenie { get; set; }


        public Room SelectedRoom { get; set; }
        public ObservableCollection<Room> Rooms {
            get => rooms;
            set
            {
                rooms = value;
                Signal();
            }
        }

        private string search;
        private bool showCleanRoom;

        public string Search
        {
            get => search;
            set
            {
                search = value;
                Signal();
                GetAllRooms();
            }
        }
        public bool ShowCleanRoom
        {
            get => showCleanRoom;
            set
            {
                showCleanRoom = value;
                Signal();
                GetAllRooms();
            }
        }




        public RegVM()
        {
            GetAllRooms();

            CreateRoom = new CommandVM(() =>
            {
                MainWindowVM.Instance.CurrentPage = new RegAdd();
            });

            EditRoom = new CommandVM(() =>
            {
                if (SelectedRoom == null)
                    return;
                MainWindowVM.Instance.CurrentPage = new RegAdd(SelectedRoom);
            });

            DeleteRoom = new CommandVM(async() =>
            {
                if (SelectedRoom == null)
                    return;

                if (SelectedRoom.StatusId == 4)
                {
                    MessageBox.Show("Ошибка! Номер не может быть удалён", "Ошибка", MessageBoxButton.OK);
                }
                else
                if (MessageBox.Show("Удалить номер?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    await DB.GetInstance().DeleteRoom(SelectedRoom.ID);
                    Rooms.Remove(SelectedRoom);
                }

            });

            Broni = new CommandVM(() => {
                MainWindowVM.Instance.CurrentPage = new GuAdd(SelectedRoom);
                
            });

            Visilenie = new CommandVM(async () =>
            {
                if (SelectedRoom == null)
                    return;

                if (MessageBox.Show("Выселить гостя?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    await DB.GetInstance().DeleteGuest(SelectedRoom.ID);
                    MainWindowVM.Instance.CurrentPage = new Guests();
                }

            });

        }

        public async void GetAllRooms()
        {
            var allRooms = await DB.GetInstance().GetRoomWithStatus();

            if (!string.IsNullOrEmpty(Search))
            {
                allRooms = new ObservableCollection<Room>(Rooms.Where(s => s.Type.Contains(Search)));
            }

            if (ShowCleanRoom)
            {
                allRooms = new ObservableCollection<Room>(Rooms.Where(s => s.Status.Title == "Чистый"));
            }

            Rooms = new ObservableCollection<Room>(allRooms);
        }

    }
}
