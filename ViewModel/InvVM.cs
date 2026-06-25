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
            private ObservableCollection<Resource> resources;
        private ObservableCollection<Applications> applications;
        private Resource selectedResource;

            public CommandVM CreateResource { get; set; }
            public CommandVM EditResource { get; set; }

            public Resource SelectedResource
            {
                get => selectedResource;
                set
                {
                    selectedResource = value;
                    Signal();
                }
            }

            public ObservableCollection<Resource> Resources
            {
                get => resources;
                set
                {
                    resources = value;
                    Signal();
                }
            }

        public ObservableCollection<Applications> Applications
        {
            get => applications;
            set
            {
                applications = value;
                Signal();
            }
        }

        public InvVM()
            {
                GetAllResources();
                GetAllApplication();

                CreateResource = new CommandVM(() =>
                {
                    MainWindowVM.Instance.CurrentPage = new AddInventory();
                });

                EditResource = new CommandVM(() =>
                {
                    if (SelectedResource == null)
                    {
                        MessageBox.Show("Выберите материал для изменения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    MainWindowVM.Instance.CurrentPage = new AddInventory(SelectedResource);
                });
            }

            

            public async void GetAllResources()
            {
                Resources = await DB.GetInstance().GetResourcesWithStaff();
            }

            public async void GetAllApplication()
            {
            Applications = await DB.GetInstance().GetApplicationWithStaff();
        }
        }
    
}

