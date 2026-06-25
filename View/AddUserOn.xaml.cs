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
            if (selectedStaff != null)
            {
                ((ProgVM)DataContext).SetStaff(selectedStaff);
            }
            else
            {
                MessageBox.Show("Staff не имеет данных");
            }
        }
        public AddUserOn(Guest selectedGuest)
        {
            InitializeComponent();
            if (selectedGuest != null) 
            {
            
                ((ProgVM)DataContext).SetGuest(selectedGuest);
            }
            else
            {
                MessageBox.Show("Гость не имеет данных");
            }
        }

    }
}
