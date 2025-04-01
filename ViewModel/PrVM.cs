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
            MainVM = MainWindowVM.Instance;

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

            DeleteProblem = new CommandVM(async() =>
            {
                if (SelectedProblem == null)
                    return;

                if (MessageBox.Show("Удалить задачу?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    await DB.GetInstance().DeleteProblem(SelectedProblem.ID);
                    Problems.Remove(SelectedProblem);
                }

            });

        }

        public async void OnAppearing()
        {
            Problems = await DB.GetInstance().GetAllProblems();
        }
    }
}
