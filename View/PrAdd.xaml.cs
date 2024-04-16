using Sanatory.Model;
using Sanatory.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sanatory.View
{
    /// <summary>
    /// Логика взаимодействия для TaskAdd.xaml
    /// </summary>
    public partial class PrAdd : Page
    {
        public PrAdd(ViewModel.MainWindowVM MainVM)
        {
            InitializeComponent();
            var vm = ((PrAddVM)DataContext);
            vm.SetMainWindowVM(MainVM);
        }

        public PrAdd(MainWindowVM mainVM, Problem selectedProblem) : this(mainVM)
        {
            ((PrAddVM)DataContext).SetEditProblem(selectedProblem);
        }
    }
}
