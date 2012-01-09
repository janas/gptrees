// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserControlNode.cs" company="Warsaw University of Technology">
//   
// </copyright>
// <summary>
//   The user control node.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForRest
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    /// <summary>
    /// The user control node.
    /// </summary>
    public partial class UserControlNode : UserControl
    {
        #region Constants and Fields

        /// <summary>
        /// The _node info.
        /// </summary>
        private readonly string _nodeInfo;

        /// <summary>
        /// The _values.
        /// </summary>
        private readonly List<string> _values;

        /// <summary>
        /// The _color.
        /// </summary>
        private Color _color;

        /// <summary>
        /// The _line width.
        /// </summary>
        private int _lineWidth;

        /// <summary>
        /// The _my area.
        /// </summary>
        private Rectangle _myArea;

        /// <summary>
        /// The _user font.
        /// </summary>
        private Font _userFont;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserControlNode"/> class.
        /// </summary>
        /// <param name="values">
        /// The values.
        /// </param>
        /// <param name="nodeInfo">
        /// The node info.
        /// </param>
        /// <param name="myArea">
        /// The my area.
        /// </param>
        /// <param name="color">
        /// The color.
        /// </param>
        public UserControlNode(List<string> values, string nodeInfo, Rectangle myArea, Color color)
        {
            this.InitializeComponent();
            this._values = values;
            this._myArea = myArea;
            this._color = color;
            this._userFont = new Font("Tahoma", 10);
            this._nodeInfo = nodeInfo;
            this._lineWidth = 1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserControlNode"/> class.
        /// </summary>
        /// <param name="values">
        /// The values.
        /// </param>
        /// <param name="nodeInfo">
        /// The node info.
        /// </param>
        /// <param name="myArea">
        /// The my area.
        /// </param>
        /// <param name="color">
        /// The color.
        /// </param>
        /// <param name="lineWidth">
        /// The line width.
        /// </param>
        public UserControlNode(List<string> values, string nodeInfo, Rectangle myArea, Color color, int lineWidth)
        {
            this.InitializeComponent();
            this._values = values;
            this._myArea = myArea;
            this._color = color;
            this._userFont = new Font("Tahoma", 10);
            this._nodeInfo = nodeInfo;
            this._lineWidth = lineWidth;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The get my area.
        /// </summary>
        /// <returns>
        /// Returns rectangle.
        /// </returns>
        public Rectangle GetMyArea()
        {
            return this._myArea;
        }

        /// <summary>
        /// The verify size.
        /// </summary>
        public void VerifySize()
        {
            Graphics g = this.CreateGraphics();
            g.SmoothingMode = SmoothingMode.AntiAlias;
            string text = string.Empty;
            for (int i = 0; i < this._values.Count; i++)
            {
                text += this._values[i] + " | ";
            }

            SizeF textRect = g.MeasureString(text, this._userFont);
            if (this.Width > (int)textRect.Width * 2)
            {
                int centerX = this.Location.X + this.Width / 2;
                this.Width = (int)textRect.Width * 2;
                this.Location = new Point(centerX - this.Width / 2, this.Location.Y);
            }

            g.Dispose();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The user control node mouse hover.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void UserControlNodeMouseHover(object sender, EventArgs e)
        {
            string result = string.Empty;
            result += this._nodeInfo + " ";
            for (int i = 0; i < this._values.Count; i++)
            {
                if (i != 0)
                {
                    result += " | ";
                }

                result += this._values[i];
            }

            this.nodeToolTip.Show(result, this);
        }

        /// <summary>
        /// The user control node paint.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void UserControlNodePaint(object sender, PaintEventArgs e)
        {
            this.BackColor = Color.White;
            var pen = new Pen(this._color, this._lineWidth);
            Graphics g = this.CreateGraphics();
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var brush = new SolidBrush(Color.Black);

            g.DrawRectangle(pen, new Rectangle(0, 0, this.Width - 1, this.Height - 1));
            for (int i = 0; i < this._values.Count; i++)
            {
                g.DrawString(
                    this._values[i], 
                    this._userFont, 
                    brush, 
                    new RectangleF(
                        i * this.Width / this._values.Count + 2, 
                        2, 
                        (this.Width / this._values.Count) - 5, 
                        this.Height - 5));
                if (i != 0)
                {
                    g.DrawLine(
                        pen, 
                        i * this.Width / this._values.Count, 
                        0, 
                        i * this.Width / this._values.Count, 
                        this.Height - 1);
                }
            }
        }

        #endregion
    }
}