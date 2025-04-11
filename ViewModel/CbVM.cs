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
    public class CbVM : BaseVM
    {
        private ObservableCollection<Cabinet> cabinets;

        private MainWindowVM MainVM;
        public CommandVM CreateCabinet { get; set; }
        public CommandVM EditCabinet { get; set; }
        public CommandVM DeleteCabinet { get; set; }
        public Staff SelectedStaff { get; set; }


        public Cabinet SelectedCabinet { get; set; }
        public ObservableCollection<Cabinet> Cabinets
        {
            get => cabinets;
            set
            {
                cabinets = value;
                Signal();
            }
        }

        public CbVM()
        {
            MainVM = MainWindowVM.Instance;

            CreateCabinet = new CommandVM(() =>
            {
                MainWindowVM.Instance.CurrentPage = new CbAdd();
            });

            EditCabinet = new CommandVM(() =>
            {
                if (SelectedCabinet == null)
                    return;
                MainWindowVM.Instance.CurrentPage = new CbAdd(SelectedCabinet);
            });

            DeleteCabinet = new CommandVM(async() =>
            {
                if (SelectedCabinet == null)
                    return;

                if (MessageBox.Show("Удалить кабинет?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    await DB.GetInstance().DeleteCabinet(SelectedCabinet.ID);
                    //Cabinets.Remove(SelectedCabinet);
                }

            });
        }

        public async void OnAppearing()
        {
            Cabinets = await DB.GetInstance().GetAllCabinets();
        }
    }
}
