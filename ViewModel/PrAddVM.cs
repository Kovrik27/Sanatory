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
        MainWindowVM MainVM;
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


            Save = new CommandVM(() =>
            {

                if (Problem.ID == 0)
                    ProblemRepository.Instance.AddProblem(Problem);
                else
                    ProblemRepository.Instance.UpdateProblem(Problem);


                MainVM.CurrentPage = new Personal(MainVM);

            });

        }

        internal void SetMainWindowVM(MainWindowVM MainVM)
        {
            this.MainVM = MainVM;
        }

        internal void SetEditProblem(Problem selectedProblem)
        {
            Problem = selectedProblem;

        }
    }
}
