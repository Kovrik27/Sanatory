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

        private MainWindowVM MainVM;
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

      

        public RegVM()
        {
            MainVM = MainWindowVM.Instance;
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
           
        }

        public async void GetAllRooms()
        {
            Rooms = await DB.GetInstance().GetAllRooms();
        }

    }
}
