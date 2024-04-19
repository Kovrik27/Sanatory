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
        public CommandVM Proceduri {  get; set; }
        public CommandVM Raspisanie { get; set; }
        public CommandVM Finansi {  get; set; }

        public MainWindowVM()
        {
            Instance = this;
            Bronirovanie = new CommandVM(() =>
            {
                OpenBronirovanie();
            });
            OpenBronirovanie();


            Personal = new CommandVM(() =>
            {
                OpenPersonal();
            });

            Gosti = new CommandVM(() =>
            {
                OpenGosti();
            });

            Proceduri = new CommandVM(() =>
            {
                OpenProceduri();
            });

            Raspisanie = new CommandVM(() =>
            {
                OpenRaspisanie();
            });

            Finansi = new CommandVM(() =>
            {
                OpenFinansi();
            });
        }

        private void OpenBronirovanie()
        {
            CurrentPage = new Registration(this);
        }
        

        private void OpenPersonal()
        {
            CurrentPage = new Personal();
        }

        private void OpenGosti()
        {
            CurrentPage = new Guests();
        }

        private void OpenProceduri()
        {
            CurrentPage = new Procedures(this);
        }

        private void OpenRaspisanie()
        {
            CurrentPage = new Schedule(this);
        }

        private void OpenFinansi()
        {
            CurrentPage = new Finance(this);
        }

    }
}
