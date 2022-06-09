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
using CsvHelper;
using System.Globalization;
using System.Runtime.Serialization.Formatters.Binary;

namespace PasswordManagerViko
{
    public partial class Main : Form
    {
        public List<PassInfo> passList = new List<PassInfo>();
        public List<PassInfo> currList = new List<PassInfo>();
        bool passwordsShown = false;
        string uName;
        public Main(string uName)
        {
            this.uName = uName;
            CheckCSVFile();
            InitializeComponent();
            LoadInfo(passList);
            currList = passList;
            
        }


        public void LoadInfo(List<PassInfo> plist)
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("name - login - password - link - comment");
            if (plist != null)
            {
                foreach (PassInfo pass in plist)
                {

                    listBox1.Items.Add(pass);
                }
            }
        }

        void CheckCSVFile()
        {
            if (!File.Exists(@$"passwords_{uName}.enc"))
            {
                if (!File.Exists(@$"passwords_{uName}.csv"))
                {
                    var myFile = File.Create(@$"passwords_{uName}.csv");
                    myFile.Close();

                }
                

            }
            else
            {

                AESHelper.AES_DecryptFile(@$"passwords_{uName}.enc", @$"passwords_{uName}.csv");
                File.Delete(@$"passwords_{uName}.enc");
                if (new FileInfo(@$"passwords_{uName}.csv").Length != 0)
                {
                    using (var reader = new StreamReader(@$"passwords_{uName}.csv"))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        while (csv.Read())
                            passList.Add(csv.GetRecord<PassInfo>());
                    }
                    if(passList[0] == null)
                    {
                        passList = null;
                    }
                }
            }
        }
        public void onAppExit()
        {
            if (passwordsShown)
            {
                foreach (PassInfo pass in currList)
                {
                    pass.password = AESHelper.AES_EncryptString(pass.password);
                }
            }
            using (var writer = new StreamWriter(@$"passwords_{uName}.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(passList);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddAccount ac = new(this);
            ac.Show();
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listBox1.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                if (Convert.ToInt32(index) != 0)
                {
                    string pwd;
                    
                    if (passwordsShown)
                    {
                        pwd = passList[Convert.ToInt32(index - 1)].password;
                    }
                    else
                    {
                        pwd = AESHelper.AES_DecryptString(passList[Convert.ToInt32(index - 1)].password);
                    }
                    Clipboard.SetText(pwd);
                    MessageBox.Show(pwd);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var a = DeepCopy(passList.Where(x => x.name.Contains(textBox1.Text)).ToList());
            
            if (a.Count() != 0)
            {
                LoadInfo(a);
                currList = a;
            }
            else
            {
                MessageBox.Show("Nothing found!");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
           
            LoadInfo(passList);
            currList = passList;
            textBox1.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3 up = new Form3(passList, this);
            up.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!passwordsShown)
            {
                var a = DeepCopy(currList);
                foreach (var item in a)
                {
                    item.password = AESHelper.AES_DecryptString(item.password);
                }
                LoadInfo(a);
                passwordsShown = true;
            }

            else
            {
                var a = DeepCopy(currList);
                foreach (var item in a)
                {
                    item.password = AESHelper.AES_EncryptString(item.password);
                }
                LoadInfo(currList);
                passwordsShown = false;
                

            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (passList != null)
            {
                onAppExit();
            }
            AESHelper.AES_EncryptFile(@$"passwords_{uName}.csv", @$"passwords_{uName}.enc");
            File.Delete(@$"passwords_{uName}.csv");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DeletePassword dp = new(passList, this);
            dp.Show();
        }

        public static T DeepCopy<T>(T item)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, item);
            stream.Seek(0, SeekOrigin.Begin);
            T result = (T)formatter.Deserialize(stream);
            stream.Close();
            return result;
        }
    }

    
}



