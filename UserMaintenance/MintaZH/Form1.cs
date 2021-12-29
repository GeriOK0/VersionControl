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
using MintaZH.Entities;

namespace MintaZH
{
    public partial class Form1 : Form
    {

        List<OlympicsResult> results = new List<OlympicsResult>();
        public Form1()
        {
            InitializeComponent();
            FileReader("Summer_olympic_Medals.csv");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void FileReader(string fileName)
        {

            StreamReader reader = new StreamReader(@"Data\"+fileName);

            int counter = 0;
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                if (counter > 0 && line.Length>1)
                {
                    string[] arr = line.Split(',');

                    OlympicsResult rs = new OlympicsResult();
                    rs.Year = Convert.ToInt32(arr[0].Trim());
                    rs.Country = arr[3].Trim();
                    rs.Medals[0] = Convert.ToInt32(arr[5]);
                    rs.Medals[1] = Convert.ToInt32(arr[6]);
                    rs.Medals[2] = Convert.ToInt32(arr[7]);
                    //ars.Position kimarad

                    results.Add(rs);



                    /* array tartalom ellenőrzés
                     int c = 1;

                     foreach (var m in rs.Medals)
                    {
                        Console.WriteLine(
                           c +" - " + m.ToString()
                        );

                        c++;
                    }; 
                    */


                }

                counter++;
            }

            Console.WriteLine(results.Count());
          
            
        }
    }
}
