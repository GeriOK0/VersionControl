using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElőzőAnyagok
{
    public partial class Form1 : Form
    {

        List<Ember> emberek = new List<Ember>();    


        public Form1()
        {
            InitializeComponent();
            tb_nem.MaxLength = 1;
            
            
        }

            private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        public void tb_szülLeave (object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;

            DateTime value;


            try
            {
                value = DateTime.Parse(tb.Text);
                Console.WriteLine("Helyes dátum");
            }
            catch (Exception ex)
            {
                tb.ResetText();
                Console.WriteLine("Hibás dátum");
                
            }

        }

        public void tb_nemLeave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            char value;


            if (tb.Text != "F" && tb.Text !="N")
            {
                tb.ResetText();
                Console.WriteLine("Hibás érték - Nem");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tb_vName.Text != "" && tb_kName.Text != "" && tb_szülIdő.Text != "" && tb_nem.Text != "")
            {
                string vn = tb_vName.Text;
                string kn = tb_kName.Text;
                DateTime szd = DateTime.Parse(tb_szülIdő.Text);
                char n = Char.Parse(tb_nem.Text);

                Ember hm = new Ember(vn, kn, szd, n);               

                emberek.Add(hm);
            }
            else
            {
                MessageBox.Show("Hiányosak az adatok.\nKérlek add meg helyesen a szükséges információkat.");
            }

            Console.WriteLine("Emberek száma: " + emberek.Count);
        }
    }
}
