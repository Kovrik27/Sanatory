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
    /// Логика взаимодействия для CbAdd.xaml
    /// </summary>
    public partial class CbAdd : Page
    {
        public CbAdd(ViewModel.MainWindowVM MainVM)
        {
            InitializeComponent();
            var vm = ((CbAddVM)DataContext);
            vm.SetMainWindowVM(MainVM);
        }

        public CbAdd(MainWindowVM mainVM, Cabinets selectedCabinets) : this(mainVM)
        {
            ((CbAddVM)DataContext).SetEditCabinets(selectedCabinets);
        }
    }
}
