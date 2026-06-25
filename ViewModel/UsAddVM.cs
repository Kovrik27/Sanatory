using Sanatory.Api;
using Sanatory.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.ViewModel
{
    public class UsAddVM : BaseVM
    {
        public User user { get; set; } = new();
        public User User
        {
            get => user;
            set
            {
                user = value;
                Signal();
            }
        }

        public List<User> Users { get; set; }

        public ObservableCollection<Role> roles { get; set; }

        public ObservableCollection<Role> Roles
        {
            get => roles;
            set
            {
                roles = value;
                Signal();
            }
        }

        public CommandVM Save { get; set; }

        public UsAddVM()
        {
            GetAllRole();

            Save = new CommandVM(async () =>
            {
                await DB.GetInstance().AddNewUser(User);
            });
        }

        public async void GetAllRole()
        {
            Roles = await DB.GetInstance().GetAllRoleUser();

        }
        internal void SetUser(User selectedUser)
        {
            User = selectedUser;
        }

    }
}
