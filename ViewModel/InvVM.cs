using Sanatory.Api;
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
    public class InvVM : BaseVM
    {
        private ObservableCollection<Resources> resources;

        public CommandVM CreateBid { get; set; }
        public CommandVM EditBid { get; set; }
        public CommandVM EditStatusBid { get; set; }



        public Problem SelectedBid { get; set; }
        public ObservableCollection<Resources> Resources
        {
            get => resources;
            set
            {
                resources = value;
                Signal();
            }
        }

        public InvVM()
        {

            //GetAllResources();

            CreateBid = new CommandVM(() =>
            {
                MainWindowVM.Instance.CurrentPage = new AddInventory();
            });

            //EditBid = new CommandVM(() =>
            //{
            //    if (SelectedBid == null)
            //        return;
            //    MainWindowVM.Instance.CurrentPage = new AddInventory(SelectedBid);
            //});

            EditStatusBid = new CommandVM(async () =>
            {
                await DB.GetInstance().DoneProblem(SelectedBid.ID);
                MessageBox.Show("Задача выполнена!");
            });

        }

        //public async void GetAllResources()
        //{
        //    Resources = await DB.GetInstance().GetAllProblems();
        //}
    }
}

