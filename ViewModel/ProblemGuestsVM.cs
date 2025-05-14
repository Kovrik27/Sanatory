using Sanatory.Api;
using Sanatory.Model;
using Sanatory.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Sanatory.ViewModel
{
    public class ProblemGuestsVM : BaseVM
    {

        public CommandVM Save { get; set; }

        private Problem problem = new();

        public Problem Problem
        {
            get => problem;
            set
            {
                problem = value;
                Signal();
            }
        }

        public ProblemGuestsVM()
        {

            Save = new CommandVM(async () =>
            {
                await DB.GetInstance().AddNewProblem(Problem);
                PatientsWindow patientsWindow = new PatientsWindow();
                patientsWindow.Show();
            });

        }
    }
}
