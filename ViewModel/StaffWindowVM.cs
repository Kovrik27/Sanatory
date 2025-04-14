using Sanatory.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sanatory.ViewModel
{
    public class StaffWindowVM : BaseVM
    {
        public static StaffWindowVM Instance { get; set; }

        private Page currentPage;

        public Page CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = value;
                Signal();
            }
        }

        public CommandVM Tasks { get; set; }


        public StaffWindowVM()
        {
            Instance = this;

            Tasks = new CommandVM(() =>
            {
                OpenTasks();
            });
        }

        private void OpenTasks()
        {
            CurrentPage = new Tasks();
        }
    }
}
