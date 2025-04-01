using Sanatory.Api;
using Sanatory.Model;
using Sanatory.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.ViewModel
{
    public class AuVM : BaseVM
    {
        public User User { get; set; } = new User();
        public string Login { get; set; }
        public string Password { get; set; }
        public List<User> users { get; set; }
        public List<User> Users
        {
            get => users;
            set
            {
                users = value;
                Signal();
            }
        }

        public CommandVM Authorization { get; set; }

        public AuVM()
        {
            Authorization = new CommandVM(async () =>
            {
                var check = await DB.GetInstance().CheckUser(User);
                if (check == null)
                {
                    return;
                }
                else
                {
                    User = new User();
                    Signal(nameof(User));
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                }

            });
        }

    }
}
