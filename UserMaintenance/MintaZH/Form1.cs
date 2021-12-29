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
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace MintaZH
{
    public partial class Form1 : Form
    {

        List<OlympicsResult> results = new List<OlympicsResult>();

        Excel.Application xlApp;
        Excel.Workbook xlWb;
        Excel.Worksheet xlSheet;


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

        //#5 Excel export
        /* Button>Click Excel generálás, feltöltés, kezelés átadás
         * Tartalom: Kiválasztott év eredményei order by Position
         * Header: Helyezés, Ország, Arany, Ezüst, Bronz
         */

        private void CreateExcel()
        {
            try
            {   //Excel indítása
                xlApp = new Excel.Application();
                //Excel workbook létrehozása
                xlWb = xlApp.Workbooks.Add(Missing.Value);
                //Új sheet létrehozása
                xlSheet = xlWb.ActiveSheet;

                CreateTable();

                //Control átadása a felhasználónak
                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex)
            {
                string errMsg = string.Format("Error: {0}\nline: {1}", ex.Message, ex.Source);
                MessageBox.Show(errMsg, "Error");

                //Hiba esetén automatikus bezárás
                xlWb.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWb = null;
                xlApp = null;

            }
        }

        private void CreateTable()
        {
            string[] headers = new string[]
            {
                "Helyezés",
                "Ország",
                "Arany",
                "Ezüst",
                "Bronz"
            };


            for (int i = 0; i < headers.Length; i++)
            {
                xlSheet.Cells[1, i + 1] = headers[i];
            }

            int selectedYear = Convert.ToInt32(comboBox1.SelectedValue);
            Console.WriteLine("Selected year: " + selectedYear);

            List<OlympicsResult> filteredResults = (from x in results
                                                    where x.Year == selectedYear
                                                    orderby x.Position
                                                    select x).ToList();

            object[,] values = new object[filteredResults.Count, headers.Length];


            int row = 0;
            foreach (OlympicsResult f in filteredResults)
            {

                values[row, 0] = f.Position;
                values[row, 1] = f.Country;
                values[row, 2] = f.Medals[0];
                values[row, 3] = f.Medals[1];
                values[row, 4] = f.Medals[2];

                row++;
            };

            Excel.Range r = xlSheet.Range[GetCell(2, 1), GetCell(1 + values.GetLength(0), values.GetLength(1))];

            r.Value2 = values;


            Excel.Range hdr = xlSheet.Range["A1", GetCell(1, values.GetLength(1))];

            hdr.Font.Bold = true;
            hdr.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            hdr.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            hdr.EntireColumn.AutoFit();
            hdr.RowHeight = 40;
            //hdr.Interior.Color = Color.LightBlue;
            //hdr.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);
        }

        private string GetCell(int x, int y)
        {
            string ExcelCoordinate = "";
            int dividend = y;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                ExcelCoordinate = Convert.ToChar(65 + modulo).ToString() + ExcelCoordinate;
                dividend = (int)((dividend - modulo) / 26);
            }

            ExcelCoordinate += x.ToString();

            return ExcelCoordinate;

        }


        //#5 Trigger
        public void ExcelGenerate(object sender, EventArgs e)
        {
            CreateExcel();
            
        }

    }
}
