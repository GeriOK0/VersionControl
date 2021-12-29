using Programtervezesi_mintak.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Programtervezesi_mintak.Entities
{
    public class Ball : Toy
    {

        public SolidBrush BallColor { get; private set; }

        public Ball(Color c)
        {
            BallColor = new SolidBrush(c);
        }

        protected override void DrawImage(Graphics g)
        {
            g.FillEllipse(BallColor, 0, 0, Width, Height);
        }

       
        
    }
}
