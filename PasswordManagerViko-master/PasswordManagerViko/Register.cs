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
    public partial class Register : Form
    {
        List<Users.UserList> users = new();
        public Register()
        {
            InitializeComponent();
        }

        void checkFile()
        {
            var newFile = File.Create(@"users.json");
            newFile.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!File.Exists(@"users.json"))
            {
                Users.Root root = new();
                Users.UserList ul = new();
                Users.UserInfo ui = new();
                ui.totalUsers = 1;
                ul.id = 1;
                ul.name = textBox1.Text;
                ul.password = BcryptHelper.HashPassword(textBox2.Text);
                users.Add(ul);
                ui.userList = users;
                root.userInfo = ui;

                using (StreamWriter file = File.CreateText(@"users.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, root);
                }

            }
            else
            {
                Users.Root users;
                using (StreamReader file = File.OpenText(@"users.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    users = (Users.Root)serializer.Deserialize(file, typeof(Users.Root));
                    users.userInfo.totalUsers++;
                    Users.UserList ul = new();
                    ul.id = users.userInfo.totalUsers;
                    ul.name = textBox1.Text;
                    ul.password = BcryptHelper.HashPassword(textBox2.Text);
                    users.userInfo.userList.Add(ul);

                    

                }

                using (StreamWriter file = File.CreateText(@"users.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, users);
                }
            }

            MessageBox.Show("Successfully!");
        }
    }
}
