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

                //CreateTable();

                //Control átadása a felhasználónak
                xlApp.Visible = true;
                xlApp.UserControl = true
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
    }
}
