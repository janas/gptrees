using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ForRest
{
    public partial class UserControlNode : UserControl
    {
        private List<string> Values;
        public Rectangle MyArea;
        public Font font;
        public bool _mark;

        public UserControlNode(List<string> values, Rectangle myArea, bool mark)
        {
            InitializeComponent();
            Values = values;
            MyArea = myArea;
            _mark = mark;
            font = new Font("Tahoma", 10);
        }

        public Rectangle GetMyArea()
        {
            return MyArea;
        }

        public void verifySize()
        {
            Graphics g = CreateGraphics();
            string text = "";
            for (int i = 0; i < Values.Count; i++)
                text += Values[i].ToString() + " | ";
            SizeF textRect = g.MeasureString(text, font);
            if (Width > (int)textRect.Width * 2)
            {
                int centerX = Location.X + Width / 2;
                Width = (int)textRect.Width * 2;
                Location = new Point(centerX - Width / 2, Location.Y);
            }
            g.Dispose();
        }

        private void UserControlNode_Paint(object sender, PaintEventArgs e)
        {
            BackColor = Color.White;
            Pen pen;
            if (_mark)
                pen = new Pen(Color.Red, 1);
            else
                pen = new Pen(Color.Black, 1);
            Graphics g = CreateGraphics();
            SolidBrush brush = new SolidBrush(Color.Black);

            g.DrawRectangle(pen, new Rectangle(0, 0, Width - 1, Height - 1));
            for (int i = 0; i < Values.Count; i++)
            {
                g.DrawString(Values[i].ToString(), font, brush,
                    new RectangleF( i * (Width) / Values.Count + 2, 2, 
                        (Width / Values.Count) - 5, Height - 5));
                if (i != 0)
                    g.DrawLine(pen, i * (Width) / Values.Count, 0, 
                        i * (Width) / Values.Count, Height-1);
            }
        }

        private void UserControlNode_MouseHover(object sender, EventArgs e)
        {
            string result = "";
            for (int i = 0; i < Values.Count; i++)
            {
                if (i != 0)
                    result += " | ";
                result += Values[i].ToString();
            }
            nodeToolTip.Show(result, this);
        }
    }
}
