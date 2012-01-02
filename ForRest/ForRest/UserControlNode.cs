using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ForRest
{
    public partial class UserControlNode : UserControl
    {
        private readonly List<string> _values;
        private readonly string _nodeInfo;
        public Rectangle MyArea;
        public Font UserFont;
        public bool Mark;

        public UserControlNode(List<string> values, string nodeInfo, Rectangle myArea, bool mark)
        {
            InitializeComponent();
            _values = values;
            MyArea = myArea;
            Mark = mark;
            UserFont = new Font("Tahoma", 10);
            _nodeInfo = nodeInfo;
        }

        public Rectangle GetMyArea()
        {
            return MyArea;
        }

        public void VerifySize()
        {
            Graphics g = CreateGraphics();
            g.SmoothingMode = SmoothingMode.AntiAlias;
            string text = "";
            for (int i = 0; i < _values.Count; i++)
                text += _values[i] + " | ";
            SizeF textRect = g.MeasureString(text, UserFont);
            if (Width > (int) textRect.Width*2)
            {
                int centerX = Location.X + Width/2;
                Width = (int) textRect.Width*2;
                Location = new Point(centerX - Width/2, Location.Y);
            }
            g.Dispose();
        }

        private void UserControlNodePaint(object sender, PaintEventArgs e)
        {
            BackColor = Color.White;
            Pen pen;
            if (Mark)
                pen = new Pen(Color.Red, 1);
            else
                pen = new Pen(Color.Black, 1);
            Graphics g = CreateGraphics();
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var brush = new SolidBrush(Color.Black);

            g.DrawRectangle(pen, new Rectangle(0, 0, Width - 1, Height - 1));
            for (int i = 0; i < _values.Count; i++)
            {
                g.DrawString(_values[i], UserFont, brush,
                             new RectangleF(i*(Width)/_values.Count + 2, 2,
                                            (Width/_values.Count) - 5, Height - 5));
                if (i != 0)
                    g.DrawLine(pen, i*(Width)/_values.Count, 0,
                               i*(Width)/_values.Count, Height - 1);
            }
        }

        private void UserControlNodeMouseHover(object sender, EventArgs e)
        {
            string result = "";
            result += _nodeInfo + " ";
            for (int i = 0; i < _values.Count; i++)
            {
                if (i != 0)
                    result += " | ";
                result += _values[i];
            }
            nodeToolTip.Show(result, this);
        }
    }
}
