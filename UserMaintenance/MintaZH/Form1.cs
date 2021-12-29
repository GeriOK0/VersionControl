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
            SetPosition();
            ComboBoxLoader();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //#2 adatbeolvasás és results feltöltése
        private void FileReader(string fileName)
        {
            //A használandó állományokat a Data mappába kell feltölteni
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

            //Console.WriteLine(results.Count());
          
            
        }

        //#3
        private void ComboBoxLoader()
        {
            List<int> Years = new List<int>();

            Years = (from x in results
                     orderby x.Year
                     select x.Year).Distinct().ToList();          

            comboBox1.DataSource = Years;
        }

        //#4
        private int GetPosition (OlympicsResult or) {

            //results lista szűrése Year == or.Year && ország != or.Country

            /*szűrt lista végigjárása és counter növelése, ha
             * aranyak száma nagyobb mint or-nél 
             * vagy
             * aranyak száma egyenlő de az ezüsté több mint or-nél
             * vagy
             * arany és ezüst száma egyenlő de a bronzé több mint or-nél
             * 
             * output counter+1
             */

            int counter = 0; //or-nél jobban teljesítő országok száma


            List<OlympicsResult> filteredResults = (from x in results
                                                    where
                                                        x.Year == or.Year &&
                                                        x.Country != or.Country
                                                    select x).ToList();

            foreach (var i in filteredResults)
            {
                if (
                    i.Medals[0] > or.Medals[0] ||
                    (i.Medals[0] == or.Medals[0] && i.Medals[1] > or.Medals[1]) ||
                    (i.Medals[0] == or.Medals[0] && i.Medals[1] == or.Medals[1] && i.Medals[2] > or.Medals[2])
                    )
                {
                    counter++;
                };
            }

            int position = counter + 1;

            return position;
        }

        private void SetPosition()
        {
            foreach (OlympicsResult o in results)
            {
                o.Position = GetPosition(o);
            }
        }

    }
}
