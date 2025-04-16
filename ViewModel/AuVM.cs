using Sanatory.Api;
using Sanatory.Model;
using Sanatory.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

                await DB.GetInstance().CheckUser(User);
                //if (check == null)
                //    return;
                //else
                //{
                //    MessageBox.Show("Ошибка! Вы не зарегистрированы");
                //}

            });
        }

    }
}
