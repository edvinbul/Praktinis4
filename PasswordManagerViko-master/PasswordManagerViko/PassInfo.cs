using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManagerViko
{
    [Serializable]
    public class PassInfo
    {

        public string name { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string link { get; set; }
        public string comment { get; set; }
        public PassInfo()
        {

        }

        public PassInfo(string name, string login, string pass, string url, string comm)
        {
            this.name = name;
            this.login = login;
            this.password = pass;
            this.link = url;
            this.comment = comm;
        }

        public override string ToString()
        {
            return $"{name} - {login} - {password} - {link} - {comment}";
        }
    }
}
