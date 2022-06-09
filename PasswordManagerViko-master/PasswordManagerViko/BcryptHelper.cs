using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace PasswordManagerViko
{
    public class BcryptHelper
    {
        public static string HashPassword(string pwd)
        {
            return BCrypt.Net.BCrypt.HashPassword(pwd);
        }

        public static bool CheckPassword(string pwd, string hashed)
        {
            return BCrypt.Net.BCrypt.Verify(pwd, hashed);
        }

    }
}
