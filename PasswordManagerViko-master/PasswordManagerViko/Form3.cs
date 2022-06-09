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
    public partial class Form3 : Form
    {
        List<PassInfo> passList = new List<PassInfo>();
        Main currForm;
        public Form3(List<PassInfo> pList, Main form)
        {
            passList = pList;
            currForm = form; 
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var a = passList.Where(x => x.name.Contains(textBox1.Text)).ToList();
            string encPass = AESHelper.AES_EncryptString(textBox2.Text);
            if (a.Count() != 0)
            {
                foreach(PassInfo p in passList)
                {
                    if(p.name.Contains(textBox1.Text))
                    {
                        p.password = encPass;
                    }
                }
                currForm.passList = this.passList;
                currForm.LoadInfo(currForm.passList);

                MessageBox.Show("Updated");


            }
            else
            {
                MessageBox.Show("Nothing found!");
            }
        }
    }
}
