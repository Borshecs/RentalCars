using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCost
{
    public class check_user
    {
        public string Login { get; set; }
        public bool IsAdmin { get;  }

        public string Status => IsAdmin ? "Admin" : "User";

        public check_user(string login, bool isAdmin)
        {
            Login = login.Trim();
            IsAdmin = isAdmin;
        }

    }
}
