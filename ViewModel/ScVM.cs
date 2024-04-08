using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.ViewModel
{
    public class ScVM : BaseVM
    {
        private MainWindowVM MainVM;
        internal void SetMainWindowVM(MainWindowVM MainVM)
        {
            this.MainVM = MainVM;
        }
    }
}
