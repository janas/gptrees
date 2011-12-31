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
    public partial class UserControlEdge : UserControl
    {
        bool LeftToRight;
        bool _mark;

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
            LeftToRight = ltr;
            _mark = mark;
        }

        private void UserControlEdge_Paint(object sender, PaintEventArgs e)
        {
            BackColor = Color.Transparent;
            Pen pen;
            if (_mark)
                pen = new Pen(Color.Red, 1);
            else
                pen = new Pen(Color.Black, 1);
            Graphics g = CreateGraphics();
            if (LeftToRight)
                g.DrawLine(pen, 0, 0,//Location.X, Location.Y,
                    Width - 1, Height - 1);
            else
                g.DrawLine(pen, Width - 1, 0,
                    0, Height - 1);
        }
    }
}
