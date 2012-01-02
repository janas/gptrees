using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ForRest
{
    public partial class UserControlEdge : UserControl
    {
        private readonly bool _leftToRight;
        public bool _mark;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; //WS_EX_TRANSPARENT
                return cp;
            }
        }

        public UserControlEdge(bool ltr, bool mark)
        {
            InitializeComponent();
            _leftToRight = ltr;
            _mark = mark;
        }

        private void UserControlEdgePaint(object sender, PaintEventArgs e)
        {
            BackColor = Color.Transparent;
            Pen pen;
            if (_mark)
                pen = new Pen(Color.Red, 1);
            else
                pen = new Pen(Color.Black, 1);
            Graphics g = CreateGraphics();
            //g.SmoothingMode = SmoothingMode.AntiAlias;
            if (_leftToRight)
                g.DrawLine(pen, 0, 0, //Location.X, Location.Y,
                           Width - 1, Height - 1);
            else
                g.DrawLine(pen, Width - 1, 0,
                           0, Height - 1);
        }
    }
}
