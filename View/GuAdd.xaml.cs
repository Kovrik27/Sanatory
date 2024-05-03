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
    /// Логика взаимодействия для GuAdd.xaml
    /// </summary>
    public partial class GuAdd : Page
    {
        public GuAdd()
        {
            InitializeComponent();          

        }

        public GuAdd(Guest selectedGuest)
        {
            InitializeComponent();
            ((GuAddVM)DataContext).SetEditGuest(selectedGuest);
        }

        public GuAdd(Room? selectedRoom)
        {
            InitializeComponent();
            ((GuAddVM)DataContext).SetRoom(selectedRoom);
        }
    }
}
