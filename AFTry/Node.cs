using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace AFTry
{
    class Node : Button
    {
        public Node(int X, int Y, GControl c) : base()
        {
            parent = c;
            Location = new Point(X, Y);
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
        bool isDown;

        public GControl parent;

        public new Point Location
        {
            get { return new Point(base.Location.X + this.Size.Width / 2, base.Location.Y + this.Size.Height / 2); }
            set { base.Location = new Point(value.X - this.Size.Width / 2, value.Y - this.Size.Height / 2); }
        }
        public Point NodePoint
        {
            get { return new Point(Location.X + (Size.Width / 2), Location.Y + (Size.Height / 2)); }
        }




        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            isDown = true;

            base.OnMouseDown(mevent);
        }
        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            if (isDown && (parent as GControl).SelectedTool == Tool.Move)
            {
                Point mouse = Parent.PointToClient(MousePosition);
                //mouse.Offset(-20, -20);
                this.Location = mouse;
            }
            base.OnMouseMove(mevent);
        }
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            isDown = false;
            base.OnMouseUp(mevent);
        }

        protected override void OnClick(EventArgs e)
        {
            parent.AddLink(this);
            //parent.G.DrawLine(new Pen(Color.DeepPink, Size.Width), this.Location, parent.Nodes[0].Location);
            base.OnClick(e);
        }

    }
}
