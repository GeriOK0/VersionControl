using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using Webszolgáltatás.Entities;
using Webszolgáltatás.MnbServiceReference;

namespace Webszolgáltatás
{
    public partial class Form1 : Form
    {
        BindingList<RateData> Rates = new BindingList<RateData>();

        BindingList<string> Currencies = new BindingList<string>();


        public Form1()
        {
            
            

            InitializeComponent();
            GetCurrencies();

            comboBox1.DataSource = Currencies;           

                      
            
            RefreshData();
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = new DateTime(2020, 01, 01);
            dateTimePicker2.Value = DateTime.Now;
        }

        private void ProcessXML(string doc)
        {
            XmlDocument xml = new XmlDocument();

            xml.LoadXml(doc);

            foreach (XmlElement item in xml.DocumentElement)
            {
                RateData rd = new RateData();
                rd.Date = DateTime.Parse(item.GetAttribute("date"));

                var childElement = (XmlElement)item.ChildNodes[0];
                if (childElement == null)
                {
                    continue;
                }

                rd.Currency = childElement.GetAttribute("curr");

                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0)
                {
                    rd.Value = value / unit;
                }


                Rates.Add(rd);

            }
        }


        private void Chart(BindingList<RateData> data)
        {
            chartRateData.DataSource = data;

            var series = chartRateData.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;

            var legend = chartRateData.Legends[0];
            legend.Enabled = false;

            var chartArea = chartRateData.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;
        }

        private void OnEdit(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void RefreshData()
        {
            
            var mnbService = new MNBArfolyamServiceSoapClient();

            var startD = dateTimePicker1.Value.ToString();
            var endD = dateTimePicker2.Value.ToString();
            var cur = comboBox1.Text;

            Console.WriteLine(@"StartD: {0}, EndD: {1}, Currency: {2}", startD, endD, cur);

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = cur,
                startDate = startD,
                endDate = endD,
            };

            var response = mnbService.GetExchangeRates(request);

            var result = response.GetExchangeRatesResult;

            Rates.Clear();

            ProcessXML(result);

            dataGridView1.DataSource = Rates;
            Chart(Rates);
        }


        private void GetCurrencies()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetCurrenciesRequestBody();
            var response = mnbService.GetCurrencies(request);

            var result = response.GetCurrenciesResult;

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(result);

            foreach (XmlElement item in xml.DocumentElement.ChildNodes[0])
            {
                string cr;                
                cr = item.InnerText;
                Currencies.Add(cr);
            }

            Console.WriteLine(Currencies.Count);


        }

    }
}
