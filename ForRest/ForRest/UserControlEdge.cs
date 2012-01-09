// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserControlEdge.cs" company="Warsaw University of Technology">
//   
// </copyright>
// <summary>
//   The user control edge.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ForRest
{
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// The user control edge.
    /// </summary>
    public partial class UserControlEdge : UserControl
    {
        #region Constants and Fields

        /// <summary>
        /// The _left to right.
        /// </summary>
        private readonly bool _leftToRight;

        /// <summary>
        /// The _color.
        /// </summary>
        private Color _color;

        /// <summary>
        /// The _line width.
        /// </summary>
        private int _lineWidth;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserControlEdge"/> class.
        /// </summary>
        /// <param name="ltr">
        /// The ltr.
        /// </param>
        public UserControlEdge(bool ltr)
        {
            this.InitializeComponent();
            this._leftToRight = ltr;
            this._color = Color.Black;
            this._lineWidth = 1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserControlEdge"/> class.
        /// </summary>
        /// <param name="ltr">
        /// The ltr.
        /// </param>
        /// <param name="color">
        /// The color.
        /// </param>
        /// <param name="lineWidth">
        /// The line width.
        /// </param>
        public UserControlEdge(bool ltr, Color color, int lineWidth)
        {
            this.InitializeComponent();
            this._leftToRight = ltr;
            this._color = color;
            this._lineWidth = lineWidth;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets CreateParams.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT
                return cp;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The user control edge paint.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void UserControlEdgePaint(object sender, PaintEventArgs e)
        {
            this.BackColor = Color.Transparent;
            var pen = new Pen(this._color, this._lineWidth);
            Graphics g = this.CreateGraphics();

            // g.SmoothingMode = SmoothingMode.AntiAlias;
            if (this._leftToRight)
            {
                g.DrawLine(pen, 0, 0, this.Width - 1, this.Height - 1);
            }
            else
            {
                g.DrawLine(pen, this.Width - 1, 0, 0, this.Height - 1);
            }
        }

        #endregion
    }
}