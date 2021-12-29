using Programtervezesi_mintak.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programtervezesi_mintak.Entities
{
    class Present : Toy
    {

        public SolidBrush BoxColor { get; private set; }
        public SolidBrush RibbonColor { get; private set; }

        public Present (Color cb, Color cr)
        {
            BoxColor = new SolidBrush(cb);
            RibbonColor = new SolidBrush(cr);
        }
        protected override void DrawImage(Graphics g)
        {
            g.FillRectangle(BoxColor, 0, 0, Width, Height);
            g.FillRectangle(RibbonColor, Left+Width/5 , 0, Width / 5, Height);
            g.FillRectangle(RibbonColor, 0, Left+ Height / 5, Width, Height/5);
        }
    }
}
