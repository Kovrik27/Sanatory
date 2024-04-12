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
    /// Логика взаимодействия для StAdd.xaml
    /// </summary>
    public partial class StAdd : Page
    {
        public StAdd(ViewModel.MainWindowVM MainVM)
        {
            InitializeComponent();
            var vm = ((StAddVM)DataContext);
            vm.SetMainWindowVM(MainVM, ListDays);
        }

        public StAdd(MainWindowVM mainVM, Staff selectedStaff) : this(mainVM)
        {
            ((StAddVM)DataContext).SetEditStaff(selectedStaff);
        }
    }
}
