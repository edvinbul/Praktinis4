using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace PasswordManagerViko
{
    public partial class Auth : Form
    {
        public Auth()
        {
            CheckFile();
            InitializeComponent();
           
        }

        void CheckFile()
        {
            if (!File.Exists(@"users.json"))
            {
               
                Register rf = new();
                rf.Show();
                this.Hide();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register rForm = new();
            rForm.Show();           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Users.Root users = null;
            Users.UserList currUser = new();
            using (StreamReader file = File.OpenText(@"users.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                users = (Users.Root)serializer.Deserialize(file, typeof(Users.Root));
                string name = textBox1.Text;
                string pwd = textBox2.Text;

                currUser = users.userInfo.userList.Find(x => x.name == name && BcryptHelper.CheckPassword(pwd, x.password));
                if (currUser != null )
                {
                    Main f1 = new(textBox1.Text);
                    f1.Show();
                }
                else
                {
                    MessageBox.Show("User not found");
                }



            }
        }
    }
}
