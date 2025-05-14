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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //аааа минус лабель блинб грустно пипец Loaded="FrameNoLabel" Navigated="FrameNoLabel"
            InitializeComponent();
            //myframe.Navigated += FrameNoLabel;
        }



        //private void FrameNoLabel(object sender, NavigationEventArgs e)
        //{
        //    if(myframe.Visibility == Visibility.Hidden)
        //    {
        //        label.Visibility = Visibility.Collapsed;
        //    }
        //    else
        //    {
        //        label.Visibility = Visibility.Visible;
        //    }
        //}
    }
}
