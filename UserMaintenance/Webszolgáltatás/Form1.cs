using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Webszolgáltatás.Entities;
using Webszolgáltatás.MnbServiceReference;

namespace Webszolgáltatás
{
    public partial class Form1 : Form
    {
        BindingList<RateData> Rates = new BindingList<RateData>();

        public Form1()
        {
            InitializeComponent();

            dataGridView1.DataSource = Rates;

            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"
            };

            var response = mnbService.GetExchangeRates(request);

            var result = response.GetExchangeRatesResult;

            ProcessXML(result);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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
    }
}
