using Sanatory.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sanatory.ViewModel
{
    public class PatientWindowVM : BaseVM
    {
        public static PatientWindowVM Instance { get; set; }

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

        public CommandVM Information {  get; set; }
        public CommandVM Procedures { get; set; }

        public PatientWindowVM()
        {
            Instance = this;

            Information = new CommandVM(() =>
            {
                OpenInformation();
            });

            Procedures = new CommandVM(() =>
            {
                OpenProcedures();
            });
        }

        private void OpenInformation()
        {
            CurrentPage = new Information();
        }

        private void OpenProcedures()
        {
            CurrentPage = new ProceduresGuest();
        }
    }
}
