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
using Sanatory.Model;

namespace Sanatory.View
{
    /// <summary>
    /// Логика взаимодействия для RegAdd.xaml
    /// </summary>
    public partial class RegAdd : Page
    {
        public RegAdd()
        {
            InitializeComponent();
        }

        public RegAdd(Room selectedRoom)
        {
            InitializeComponent();
            ((RegAddVM)DataContext).SetEditRoom(selectedRoom);
        }

    }
}
