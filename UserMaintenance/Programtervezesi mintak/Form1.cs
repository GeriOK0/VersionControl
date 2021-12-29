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

        private List<Ball> _balls = new List<Ball>();

        private BallFactory _factory;
        private BallFactory Factory
        {
            get { return _factory; }
            set { _factory = value; }
        }
        

        public Form1()
        {
            InitializeComponent();
            Factory = new BallFactory();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            createTimer.Start();
            conveyorTimer.Start();
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            var ball = Factory.CreateNew();
            _balls.Add(ball);
            ball.Left = -ball.Width;
            mainPanel.Controls.Add(ball);
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var maxPosition = 0;

            foreach (var b in _balls)
            {
                b.MoveBall();

                if (b.Left > maxPosition)
                {
                    maxPosition = b.Left;
                }
            }

            if (maxPosition >= 1000)
            {
                Ball fst = _balls.First();

                mainPanel.Controls.Remove(fst);
                _balls.Remove(fst);

                //Console.WriteLine(_balls.Count);
            }
        }
    }
}
