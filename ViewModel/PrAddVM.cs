using Sanatory.Api;
using Sanatory.Model;
using Sanatory.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sanatory.ViewModel
{
    public class PrAddVM : BaseVM
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
        public PrAddVM()
        {

            Save = new CommandVM(async() =>
            {

                if (Problem.ID == 0)
                   await DB.GetInstance().AddNewProblem(Problem);
                else
                   await DB.GetInstance().EditProblem(Problem);

                MainWindowVM.Instance.CurrentPage = new Processes();

            });

        }
      

        internal void SetEditProblem(Problem selectedProblem)
        {
            Problem = selectedProblem;

        }
    }
}
