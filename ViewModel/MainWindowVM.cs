using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sanatory.ViewModel
{
    public class MainWindowVM : BaseVM
    {
        private Page currentPage;

        public Page CurrentPage { get => currentPage;
        set
            {
                currentPage = value;
                Signal();
            }
        }

        public CommandVM Pages {  get; set; }

        public MainWindowVM() 
        {
            Pages = new CommandVM(() =>
            {
                OpenPages();
            });
            
            

        }

    }
}
