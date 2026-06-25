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
    public class MainWindowVM : BaseVM
    {
        public static MainWindowVM Instance { get; private set; }

        private Page currentPage;

        public Page CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = value;
                Signal();
            }
        }

        public CommandVM Bronirovanie { get; set; }
        public CommandVM Personal {  get; set; }
        public CommandVM Gosti {  get; set; }
        public CommandVM Processes {  get; set; }
        public CommandVM Raspisanie { get; set; }
        public CommandVM Feedbacks { get; set; }
        public CommandVM Reports { get; set; }
        public CommandVM Inventory { get; set; }


        public MainWindowVM()
        {
            Instance = this;
            Bronirovanie = new CommandVM(() =>
            {
                OpenBronirovanie();
            });


            Personal = new CommandVM(() =>
            {
                OpenPersonal();
            });

            Gosti = new CommandVM(() =>
            {
                OpenGosti();
            });

            Processes = new CommandVM(() =>
            {
                OpenProcesses();
            });

            Raspisanie = new CommandVM(() =>
            {
                OpenRaspisanie();
            });

            Feedbacks = new CommandVM(() =>
            {
                OpenFeedbacks();
            });

            Reports = new CommandVM(() =>
            {
                OpenReports();
            });

            Inventory = new CommandVM(() =>
            {
                OpenInventory();
            });
        }

        private void OpenInventory()
        {
            CurrentPage = new InventoryPage();
        }

        private void OpenReports()
        {
            CurrentPage = new ReportsPage();
        }

        private void OpenFeedbacks()
        {
           CurrentPage = new FeedbacksPage();
        }

        private void OpenBronirovanie()
        {
            CurrentPage = new Registration();
        }
        

        private void OpenPersonal()
        {
            CurrentPage = new Personal();
        }

        private void OpenGosti()
        {
            CurrentPage = new Guests();
        }

        private void OpenProcesses()
        {
            CurrentPage = new Processes();
        }

        private void OpenRaspisanie()
        {
            CurrentPage = new Schedule();
        }

       private void OpenUsersList()
        {
            CurrentPage = new UsersList();
        }

    }
}
