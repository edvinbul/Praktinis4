using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasswordManagerViko
{
    public partial class AddAccount : Form
    {
        Main cForm;
        public AddAccount(Main currForm)
        {
            cForm = currForm;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(textBox1.Text) && !String.IsNullOrEmpty(textBox2.Text) && !String.IsNullOrEmpty(textBox3.Text) && !String.IsNullOrEmpty(textBox4.Text) && !String.IsNullOrEmpty(textBox5.Text))
            {
                string pwd = AESHelper.AES_EncryptString(textBox3.Text);
                if (cForm.passList == null) cForm.passList = new List<PassInfo>();
                cForm.passList.Add(new PassInfo(textBox1.Text, textBox2.Text, pwd, textBox4.Text, textBox5.Text));
                cForm.LoadInfo(cForm.passList);
                MessageBox.Show("Successfully");
            }
            else
            {
                MessageBox.Show("Fill all fields!");
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Random random = new Random();

            string newPwd = "";
            int pwdLength = random.Next(10, 24);
            for (int i = 0; i < pwdLength; i++)
            {
                newPwd += (char)random.Next(33, 122);
            }

            this.textBox3.Text = newPwd;


        }
    }
}
