using Programtervezesi_mintak.Abstractions;
using Programtervezesi_mintak.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Programtervezesi_mintak
{
    
    public partial class Form1 : Form
    {

        private List<Toy> _toys = new List<Toy>();

        private IToyFactory _factory;
        private IToyFactory Factory
        {
            get { return _factory; }
            set { _factory = value;
                DisplayNext();
            }
        }

        private Toy _nextToy;
        

        public Form1()
        {
            InitializeComponent();
            //Factory = new BallFactory();
            Factory = new CarFactory();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            createTimer.Start();
            conveyorTimer.Start();
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            var ball = Factory.CreateNew();
            _toys.Add(ball);
            ball.Left = -ball.Width;
            mainPanel.Controls.Add(ball);
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var maxPosition = 0;

            foreach (var b in _toys)
            {
                b.MoveToy();

                if (b.Left > maxPosition)
                {
                    maxPosition = b.Left;
                }
            }

            if (maxPosition >= 1000)
            {
                Toy fst = _toys.First();

                mainPanel.Controls.Remove(fst);
                _toys.Remove(fst);

                //Console.WriteLine(_balls.Count);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Factory = new CarFactory();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Factory = new BallFactory();
        }


        private void DisplayNext()
        {
            if (_nextToy != null)
            {
                Controls.Remove(_nextToy);
            }

            _nextToy = Factory.CreateNew();
            _nextToy.Top = label1.Top + label1.Height + 20;
            _nextToy.Left = label1.Left;
            Controls.Add(_nextToy);
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            var colorPicker = new ColorDialog();

            colorPicker.Color = button.BackColor;

            if (colorPicker.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            else
            {
                button.BackColor = colorPicker.Color;
            }                
            

        }
    }
}
