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
    public class AppVM : BaseVM
    {
        private ObservableCollection<Applications> applications;
        private Applications selectedApplication;

        public CommandVM CreateApplication { get; set; }
        public CommandVM EditApplication { get; set; }

        public Applications SelectedApplications
        {
            get => selectedApplication;
            set
            {
                selectedApplication = value;
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

        public AppVM()
        {
            GetAllApplications();

            CreateApplication = new CommandVM(() =>
            {
                MainWindowVM.Instance.CurrentPage = new AddApplication();
            });

            EditApplication = new CommandVM(() =>
            {
                if (SelectedApplications == null)
                {
                    MessageBox.Show("Выберите заявку на материал для изменения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                MainWindowVM.Instance.CurrentPage = new AddApplication(SelectedApplications);
            });
        }



        public async void GetAllApplications()
        {
            Applications = await DB.GetInstance().GetApplicationWithStaff();
        }
    }

}

