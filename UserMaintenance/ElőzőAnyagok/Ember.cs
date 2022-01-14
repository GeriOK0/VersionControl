using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ElőzőAnyagok
{
    class Ember : Button
    {
        public string keresztnév;
        public string vezetéknév;
        public DateTime szuletesiDatum {get; set;}
        public char nem { get; set; }


        private string teljesNév
        {
            get {

                return this.keresztnév + "-" + this.vezetéknév;
            }
        }

        private int kor
        {
            get {
                DateTime nw = DateTime.Now;
                DateTime br = this.szuletesiDatum;

                int diff = nw.Year - br.Year;

                if (nw.Month>br.Month || (nw.Month == br.Month && nw.Day>=br.Day))
                {
                    return diff + 1;
                }
                else
                {
                    return diff;
                }          
               
            }
        }

        // 7 konstruktor készítése

        public Ember (string vnév, string knév, DateTime szdátum, char n)
        {
            this.vezetéknév = vnév;
            this.keresztnév = knév;
            this.szuletesiDatum = szdátum;
            this.nem = n;
            

            this.Width = 60;
            this.Height = this.Width;

            switch (this.nem)
            {
                case 'F':
                    this.BackColor = Color.Blue;
                    break;
                case 'N':
                    this.BackColor = Color.Pink;
                    break;
                default:
                    this.BackColor = Color.White;
                    break;
            }
        }

        public void ButtonClick(object sender, EventArgs e)
        {
            MessageBox.Show("Teljes név: " + this.teljesNév + "\nKor: " + this.kor.ToString());

        }

        public string GetMonogram()
        {
            string first = this.vezetéknév.Substring(1).ToUpper();
            string second = this.keresztnév.Substring(1).ToUpper();
            return first + second;
        }


    }
}
