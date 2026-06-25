using Sanatory.ViewModel;
using System.Windows.Controls;

namespace Sanatory.View
{
    public partial class ServiceRequestPage : Page
    {
        public ServiceRequestPage(int guestId)
        {
            InitializeComponent();
            DataContext = new ServiceRequestPageVM(guestId);
        }
    }
}