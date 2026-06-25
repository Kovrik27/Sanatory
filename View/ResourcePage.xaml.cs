using Sanatory.ViewModel;
using System.Windows.Controls;

namespace Sanatory.View
{
    public partial class ResourcePage : Page
    {
        public ResourcePage(int staffId)
        {
            InitializeComponent();
            DataContext = new ResourcePageVM(staffId);
        }
    }
}
