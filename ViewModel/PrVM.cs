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
    public class PrVM : BaseVM
    {
        private ObservableCollection<Problem> problems;

        private MainWindowVM MainVM;
        public CommandVM CreateProblem { get; set; }
        public CommandVM EditProblem { get; set; }
        public CommandVM EditStatusProblem { get; set; }



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
            MainVM = MainWindowVM.Instance;
            GetAllProblems();

            CreateProblem = new CommandVM(() =>
            {
                MainWindowVM.Instance.CurrentPage = new PrAdd();
            });

            EditProblem = new CommandVM(() =>
            {
                if (SelectedProblem == null)
                    return;
                MainWindowVM.Instance.CurrentPage = new PrAdd(SelectedProblem);
            });

            EditStatusProblem = new CommandVM(async () =>
            {
                await DB.GetInstance().DoneProblem(SelectedProblem.ID);
                MessageBox.Show("Задача выполнена!");
            });

        }

        public async void GetAllProblems()
        {
            Problems = await DB.GetInstance().GetAllProblems();
        }
    }
}
