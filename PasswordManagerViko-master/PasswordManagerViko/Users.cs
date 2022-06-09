using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManagerViko
{
    class Users
    {
        public class UserList
        {
                public int id { get; set; }
                public string name { get; set; }
                public string password { get; set; }
        }

        public class UserInfo
        {
                public int totalUsers { get; set; }
                public List<UserList> userList { get; set; }
        }

        public class Root
        {
                public UserInfo userInfo { get; set; }
        }
    }
}
