using Sanatory.Model;
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
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
            DataContext = this;

            //TextBlock tb = new TextBlock();
            //Hyperlink hyperlink = new Hyperlink();
            //Run run = new Run();
            //run.Text = "Ещё не зарегистрированы?";
            //hyperlink.NavigateUri = new Uri("AddNewUser.xaml");
            //hyperlink.Inlines.Add(run);
            //tb.Inlines.Add(hyperlink);
        }



        private void AuthorizationButton(object sender, RoutedEventArgs e)
        {
            AddNewUser addNewUser = new AddNewUser();
            addNewUser.ShowDialog();
        }

        private void PathButton(object sender, RoutedEventArgs e)
        {
            string username = UserTextBox.Text;

            if (username == "Admin")
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.ShowDialog();
                this.Close();
            }
        }
    }
}
