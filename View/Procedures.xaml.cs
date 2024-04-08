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
    /// Логика взаимодействия для Procedures.xaml
    /// </summary>
    public partial class Procedures : Page
    {
        public Procedures(ViewModel.MainWindowVM MainVM)
        {
            InitializeComponent();

            var vm = DataContext as ProVM;
            vm?.SetMainWindowVM(MainVM);
        }
    }
}
