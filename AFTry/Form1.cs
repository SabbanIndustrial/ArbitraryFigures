using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// Старый вариант перечисления инструментов.
/// </summary>
public enum Mode
{
    NodeReposition = 0,
    AddingNodes,
    AddingLinks,
    RemovingNodes,
    RemovingLinks,
    AddingMoreLinks
}


namespace AFTry
{/// <summary>
/// Главная форма.
/// Какой-то говнокод, который мне лень удалять.
/// </summary>
    public partial class Form1 : Form
    {
        public Graphics G { get; set; }
        private Mode current;
        public Mode Current
        {
            get
            {
                return current;
            }
            set
            {
                Text = $"Текущий режим: {current.ToString()}";
                current = value;
            }
        }
        List<Node> nodes = new List<Node>();

        //public List<Links> Lnk
        //{
        //    get { return links; }
        //    set { links = value; }
        //}

        List<Links> links = new List<Links>();
        public Form1()
        {
            InitializeComponent();
            G = this.CreateGraphics();

        }


        private void ClearGraphics(object sender, EventArgs e)
        {
            G.Clear(this.BackColor);
        }

        private void NodeClick(object sender, EventArgs e)
        {
            if (Current == Mode.AddingMoreLinks)
            {
                Current = Mode.AddingLinks;
                links.Last<Links>().AddConnection(sender as Node);
            }
            if (Current == Mode.AddingLinks)
            {
                Current = Mode.AddingMoreLinks;
                links.Add(new Links((Node)sender as Node));
            }
        }

        private void CreateLink()
        {

        }



        private void CreateNewNode(object sender, EventArgs e)
        {
            if (Current == Mode.AddingNodes)
            {
                //nodes.Add(new Node(0,0));
                //Point mouse = PointToClient(MousePosition);
                //mouse.Offset(-20, -20);
                //nodes.Last<Node>().Location = mouse;
                //nodes.Last<Node>().Text = $"node {nodes.Count}";
                //this.Controls.Add(nodes.Last<Node>());
            }
        }

        private void chngd(object sender, EventArgs e)
        {

            gControl1.SelectedTool = (Tool)numericUpDown1.Value;
        }
        Graphics g1;
        private void tryline(object sender, EventArgs e)
        {
            //g1 = button1.CreateGraphics();
            Text = $"{e.ToString()}";
            //g1.DrawLine(new Pen(Color.DeepPink, 10f), new Point(0, 0), new Point(button1.Size));
            // ControlPaint.DrawButton(CreateGraphics(), new Rectangle(10, 10, 90, 90), ButtonState.Flat);

        }

        private void trylinePaint(object sender, PaintEventArgs e)
        {
            //Point n1 = new Point(300, 300);
            //Point n2 = new Point(100, 100);

            //Point swapHelp;
            //if (n1.X > n2.X)
            //{
            //    swapHelp = n1;
            //    n1 = n2;
            //    n2 = swapHelp;

            //}


            //button1.Size = new Size(Math.Abs(n1.X - n2.X), Math.Abs(n1.Y - n2.Y));

            //Point p1 = new Point(0, n1.Y < n2.Y ? 0 : button1.Size.Height);
            //Point p2 = new Point(button1.Size.Width, n1.Y < n2.Y ? button1.Size.Height : 0);

            //e.Graphics.DrawLine(new Pen(Color.DeepPink, 10f), p1, p2);

            //GraphicsPath buttonPath = new GraphicsPath();
            //buttonPath.AddLine(p1, p2);
            //buttonPath.Widen(new Pen(Color.Black, 10f));
            //button1.Region = new Region(buttonPath);

            //button1.Location = new Point(n1.X < n2.X ? n1.X : n2.X, n1.Y < n2.Y ? n1.Y : n2.Y);

            //Text = $"{e.ToString()}";
        }
    }

    struct Links
    {
        public Node MainNode { get; set; }
        List<Node> connectedNodes;

        public Links(Node mainNode)
        {
            MainNode = mainNode;
            connectedNodes = new List<Node>();
        }

        public void AddConnection(Node connected)
        {
            connectedNodes.Add(connected);
        }
    }
}
