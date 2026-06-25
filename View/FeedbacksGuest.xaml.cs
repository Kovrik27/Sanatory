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
    /// Логика взаимодействия для FeedbacksGuest.xaml
    /// </summary>
    public partial class FeedbacksGuest : Page
    {
        public FeedbacksGuest()
        {
            InitializeComponent();
        }

        public FeedbacksGuest(Feedback selectedFeedback)
        {
            InitializeComponent();
            ((FdGuVM)DataContext).SetEditFeedback(selectedFeedback);
        }
        public FeedbacksGuest(User selectedUser)
        {
            InitializeComponent();
            ((FdGuVM)DataContext).SetUser(selectedUser);
        }
    }
}
