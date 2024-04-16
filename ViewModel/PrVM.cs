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
    public class PrVM : BaseVM
    {
        private ObservableCollection<Problem> problems;

        private MainWindowVM MainVM;
        public CommandVM CreateProblem { get; set; }
        public CommandVM EditProblem { get; set; }
        public CommandVM DeleteProblem { get; set; }


        public Problem SelectedProblem { get; set; }
        public ObservableCollection<Problem> Problems
        {
            get => problems;
            set
            {
                problems = value;
                Signal();
            }
        }

        public PrVM()
        {
            string sql = "SELECT * FROM Problem";

            Problems = new ObservableCollection<Problem>(ProblemRepository.Instance.GetAllProblem(sql));



            CreateProblem = new CommandVM(() =>
            {
                MainVM.CurrentPage = new PrAdd(MainVM);
            });

            EditProblem = new CommandVM(() => {
                if (SelectedProblem == null)
                    return;
                MainVM.CurrentPage = new PrAdd(MainVM, SelectedProblem);
            });

            DeleteProblem = new CommandVM(() =>
            {
                if (SelectedProblem == null)
                    return;

                if (MessageBox.Show("Удалить задачу?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    ProblemRepository.Instance.Remove(SelectedProblem);
                    Problems.Remove(SelectedProblem);
                }

            });



        }


        internal void SetMainWindowVM(MainWindowVM MainVM)
        {
            this.MainVM = MainVM;
        }



    }
}
