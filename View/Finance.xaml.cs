﻿using Sanatory.ViewModel;
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
    /// Логика взаимодействия для Finance.xaml
    /// </summary>
    public partial class Finance : Page
    {
        public Finance(ViewModel.MainWindowVM MainVM)
        {
            InitializeComponent();

            var vm = DataContext as FinVM;
            vm?.SetMainWindowVM(MainVM);
        }
    }
}
