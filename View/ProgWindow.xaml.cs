using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
using Spire.Doc.Formatting;

namespace Sanatory.View
{
    /// <summary>
    /// Логика взаимодействия для ProgWindow.xaml
    /// </summary>
    public partial class ProgWindow : Window
    {
        public ProgWindow()
        {
            InitializeComponent();
            myframe.Navigated += FrameNavigated;
            FrameNoThis(null, null);
        }

        private void FrameNavigated(object sender, NavigationEventArgs e)
        {
            FrameNoThis(null, null);
        }

        private void FrameNoThis(object sender, RoutedEventArgs e)
        {
            if (myframe.Content != null)
            {
                this.Visibility = Visibility.Collapsed;

            }
            else
            {
                this.Visibility = Visibility.Visible;
            }
        }


    }
}


