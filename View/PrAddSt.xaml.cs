﻿using Sanatory.Model;
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
    /// Логика взаимодействия для PrAddSt.xaml
    /// </summary>
    public partial class PrAddSt : Page
    {
        public PrAddSt(Staff selectedStaff)
        {
            InitializeComponent();
            ((StAddVM)DataContext).SetStaff(selectedStaff);
        }
    }
}
