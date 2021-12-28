using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace Excel_generálás
{
    public partial class Form1 : Form
    {

        RealEstateEntities context = new RealEstateEntities();
        List<Flat> Flats;

        Excel.Application xlApp;
        Excel.Workbook xlWb;
        Excel.Worksheet xlSheet;



        public Form1()
        {
            InitializeComponent();
            LoadData();
            CreateExcel();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void LoadData()
        {

            Flats = context.Flats.ToList();

        }

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
                "Kód",
                "Eladó",
                "Oldal",
                "Kerület",
                "Lift",
                "Szobák száma",
                "Alapterület (m2)",
                "Ár (mFt)",
                "Négyzteméter ár (Ft/m2)"
            };


            for (int i = 0; i < headers.Length; i++)
            {
                xlSheet.Cells[1, i + 1] = headers[i];
            }

            object[,] values = new object[Flats.Count, headers.Length];


            int row = 0;
            foreach (Flat f in Flats)
            {

                string ev;
                
                if (f.Elevator)                    
                {
                    ev = "Van";
                }
                else
                {
                    ev = "Nincs";
                }

                values[row, 0] = f.Code;
                values[row, 1] = f.Vendor;
                values[row, 2] = f.Side;
                values[row, 3] = f.District;
                values[row, 4] = ev;
                values[row, 5] = f.NumberOfRooms;
                values[row, 6] = f.FloorArea;
                values[row, 7] = f.Price;
                values[row, 8] = string.Format("={0}/{1}*1000000",GetCell(row+2,8), GetCell(row+2,7));

                row++;
            };

            Excel.Range r = xlSheet.Range[GetCell(2, 1), GetCell(1 + values.GetLength(0), values.GetLength(1))];

            r.Value2 = values;


            Excel.Range hdr = xlSheet.Range["A1", GetCell(1,values.GetLength(1))];

            hdr.Font.Bold = true;
            hdr.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            hdr.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            hdr.EntireColumn.AutoFit();
            hdr.RowHeight = 40;
            hdr.Interior.Color = Color.LightBlue;
            hdr.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);


        }

        private string GetCell (int x, int y)
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
    }
}
