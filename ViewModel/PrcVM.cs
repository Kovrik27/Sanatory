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
    public class PrcVM : BaseVM
    {
        private ObservableCollection<Procedure> procedures;

        private MainWindowVM MainVM;
        public CommandVM CreateProcedure { get; set; }
        public CommandVM EditProcedure { get; set; }
        public CommandVM DeleteProcedure { get; set; }


        public Procedure SelectedProcedure { get; set; }
        public ObservableCollection<Procedure> Procedures
        {
            get => procedures;
            set
            {
                procedures = value;
                Signal();
            }
        }

        public PrcVM()
        {
            string sql = "SELECT * FROM Procedures";

            Procedures = new ObservableCollection<Procedure>(ProceduresRepository.Instance.GetAllProcedures(sql));



            CreateProcedure = new CommandVM(() =>
            {
                MainVM.CurrentPage = new PrcAdd(MainVM);
            });

            EditProcedure = new CommandVM(() => {
                if (SelectedProcedure == null)
                    return;
                MainVM.CurrentPage = new PrcAdd(MainVM, SelectedProcedure);
            });

            DeleteProcedure = new CommandVM(() =>
            {
                if (SelectedProcedure == null)
                    return;

                if (MessageBox.Show("Удалить процедуру?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    ProceduresRepository.Instance.Remove(SelectedProcedure);
                    Procedures.Remove(SelectedProcedure);
                }

            });



        }


        internal void SetMainWindowVM(MainWindowVM MainVM)
        {
            this.MainVM = MainVM;
        }



    }
}
