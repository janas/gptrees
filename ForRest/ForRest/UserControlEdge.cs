using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ForRest
{
    public partial class UserControlEdge : UserControl
    {
        private readonly bool _leftToRight;
        private Color _color;
        private int _lineWidth;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; //WS_EX_TRANSPARENT
                return cp;
            }
        }

        public UserControlEdge(bool ltr)
        {
            InitializeComponent();
            _leftToRight = ltr;
            _color = Color.Black;
            _lineWidth = 1;
        }

        public UserControlEdge(bool ltr, Color color, int lineWidth)
        {
            InitializeComponent();
            _leftToRight = ltr;
            _color = color;
            _lineWidth = lineWidth;
        }

        private void UserControlEdgePaint(object sender, PaintEventArgs e)
        {
            BackColor = Color.Transparent;
            Pen pen = new Pen(_color, _lineWidth);
            Graphics g = CreateGraphics();
            //g.SmoothingMode = SmoothingMode.AntiAlias;
            if (_leftToRight)
                g.DrawLine(pen, 0, 0,
                           Width - 1, Height - 1);
            else
                g.DrawLine(pen, Width - 1, 0,
                           0, Height - 1);
        }
    }
}
