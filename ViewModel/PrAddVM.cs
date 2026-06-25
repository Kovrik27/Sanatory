using Sanatory.Api;
using Sanatory.Model;
using Sanatory.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sanatory.ViewModel
{
    public class PrAddVM : BaseVM
    {
        public CommandVM Save { get; set; }

        private ObservableCollection<StatusProblem> statusesproblem;
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

        public ObservableCollection<StatusProblem> StatusesProblem
        {
            get => statusesproblem;
            set
            {
                statusesproblem = value;
                Signal();
            }
        }

        public PrAddVM()
        {
            GetAllStatusesProblem();

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

        private async void GetAllStatusesProblem()
        {
            StatusesProblem = await DB.GetInstance().GetAllStatusesProblem();
        }
    }
}
