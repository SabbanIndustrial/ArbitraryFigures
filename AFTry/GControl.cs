using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AFTry
{
    class GControl : Panel
    {
        public Tool SelectedTool;

        public GControl() : base()
        {
            
            SelectedTool = Tool.AddNode;

        }

        public List<Node> Nodes = new List<Node>();

        public List<LinkStr> Links = new List<LinkStr>();

        public List<Link> LinkCntr = new List<Link>();

        public Graphics G;

        protected override void OnMouseClick(MouseEventArgs e)
        {
            G = this.CreateGraphics();
            if (SelectedTool == Tool.AddNode)
            {
                Nodes.Add(new Node(0, 0, this));
                Point mouse = PointToClient(MousePosition);
                //mouse.Offset(-20, -20);
                Nodes.Last<Node>().Location = mouse;
                Nodes.Last<Node>().Text = $"node {Nodes.Count}";
                this.Controls.Add(Nodes.Last<Node>());
            }


            base.OnMouseClick(e);
        }

        bool acceptLink = false;
        public void AddLink(Node a)
        {
            if (acceptLink == false)
            {
                
                Links.Add(new LinkStr(a.Location));
                acceptLink = true;
            }
            else
            {
                Links.Last().B = a.Location;
                LinkCntr.Add(new Link(Links.Last().A, Links.Last().B, this));
                Controls.Add(LinkCntr.Last());
                acceptLink = false;
              //  DrawLinks();
            }
            

        }

        public void Invalidate()
        {
            base.Invalidate();



        }
        public void DrawLinks()
        {
            for (int i = 0; i < Links.Count; i++)
            {
                Controls.Remove(LinkCntr[i]);
              //  LinkCntr[i].LinkNodes()

            }
        }

    }
    public enum Tool
    {
        None = 0,
        AddNode,
        Move,
    }
    public class LinkStr
    {
        public Point A, B;
        public LinkStr(Point a)
        {
            A = a;
            B = a;
        }


    }

}
