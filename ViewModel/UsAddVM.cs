using Sanatory.Api;
using Sanatory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.ViewModel
{
    public class UsAddVM : BaseVM
    {
        public User User { get; set; } = new();
        public List<User> Users { get; set; }

        public CommandVM Save { get; set; }

        public UsAddVM()
        {
            Save = new CommandVM(async () =>
            {
                await DB.GetInstance().AddNewUser(User);
            });
        }


        internal void SetUser(User selectedUser)
        {
            User = selectedUser;
        }

    }
}
