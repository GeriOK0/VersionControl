using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserMaintenance.Entities;

namespace UserMaintenance
{
    public partial class Form1 : Form
    {

        //#6/1
        BindingList<User> users = new BindingList<User>();


        public Form1()
        {
            InitializeComponent();
            label1.Text = Resource1.FullName;            
            button1.Text = Resource1.Add;
            button2.Text = Resource1.Export;

            //#6/2
            listBox1.DataSource = users;
            listBox1.ValueMember = "ID";
            listBox1.DisplayMember = "FullName";

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //#6/3
        private void AddUser(object sender, EventArgs e)
        {
            
            var u = new User()
            {
                FullName = textBox1.Text,                
            };
            users.Add(u);        
        }

        //#9
        private void ExportCSV(object sender, EventArgs e)
        {
            SaveFileDialog sfDial1 = new SaveFileDialog();
            
            string filter = "CSV file (*.csv)|*.csv| All Files (*.*)|*.*";
            sfDial1.Filter = filter;

            string fileName = sfDial1.FileName;

            

            if (sfDial1.ShowDialog() == DialogResult.OK)                
            {
                filter = sfDial1.FileName;

                StreamWriter sw = new StreamWriter(filter);

                foreach (User s in users) {

                    sw.WriteLine("{0},{1},", s.ID, s.FullName);                    
                }

                sw.Close();       
            }
        }







    }
}
