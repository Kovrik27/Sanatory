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
using System.Windows.Shapes;

namespace Sanatory.View
{
    /// <summary>
    /// Логика взаимодействия для AddUserOn.xaml
    /// </summary>
    public partial class AddUserOn : Window
    {
        public AddUserOn(Staff selectedStaff)
        {
            InitializeComponent();
            ((ProgVM)DataContext).SetStaff(selectedStaff);
        }
        public AddUserOn(Guest selectedGuest)
        {
            InitializeComponent();
            ((ProgVM)DataContext).SetGuest(selectedGuest);
        }

    }
}
