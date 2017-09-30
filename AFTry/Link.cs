using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AFTry
{
    class Link : Button
    {
        public Node N1 { get; set; }
        public Node N2 { get; set; }

        public bool showLink = true;

        GControl parent;
        Point n1;
        Point n2;

        public Link(Point n1, Point n2,GControl c) : base()
        {
            this.n1 = n1;
            this.n2 = n2;
            parent = c;
            int radius = 40;
            Size = new Size(radius, radius);
            BackColor = Color.CadetBlue;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;

            GraphicsPath buttonPath = new GraphicsPath();
            Rectangle newRectangle = this.ClientRectangle;
            buttonPath.AddEllipse(newRectangle);
            Region = new Region(buttonPath);
        }

        public void LinkNodes(Point n1, Point n2)
        {
            this.n1 = n1;
            this.n2 = n2;
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            if (showLink)
            {
                base.OnPaint(e);


                Point swapHelp;
                if (n1.X > n2.X)
                {
                    swapHelp = n1;
                    n1 = n2;
                    n2 = swapHelp;

                }


                this.Size = new Size(Math.Abs(n1.X - n2.X), Math.Abs(n1.Y - n2.Y));
                if (Size.Width>0||Size.Height>0) {
                    Point p1 = new Point(0, n1.Y < n2.Y ? 0 : this.Size.Height);
                    Point p2 = new Point(this.Size.Width, n1.Y < n2.Y ? this.Size.Height : 0);

                    e.Graphics.DrawLine(new Pen(Color.DeepPink, 10f), p1, p2);

                    GraphicsPath buttonPath = new GraphicsPath();

                    buttonPath.AddLine(p1, p2);
                    buttonPath.Widen(new Pen(Color.Black, 10f));

                    this.Region = new Region(buttonPath);

                    this.Location = new Point(n1.X < n2.X ? n1.X : n2.X, n1.Y < n2.Y ? n1.Y : n2.Y);
                }
            }

        }

        protected override void OnClick(EventArgs e)
        {
            Parent.Parent.Text = $"dsddasdf67";
            base.OnClick(e);
        }


    }
}
