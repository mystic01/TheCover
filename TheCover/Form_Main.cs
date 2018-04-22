using System;
using System.Drawing;
using System.Windows.Forms;

namespace TheCover
{
    public partial class Form_Main : Form
    {
        public Form_Main()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        private const int cGrip = 12;      // Grip size
        private const int cCaption = 32;   // Caption bar height;

        public static readonly int WM_NCHITTEST = 0x84;
        public static readonly int HTTOPLEFT = 13;
        public static readonly int HTBOTTOMRIGHT = 17;

        protected override void OnPaint(PaintEventArgs e)
        {
            var rc = new Rectangle(this.ClientSize.Width - cGrip, this.ClientSize.Height - cGrip, cGrip, cGrip);
            //ControlPaint.DrawSizeGrip(e.Graphics, this.BackColor, rc);
            rc = new Rectangle(0, 0, this.ClientSize.Width, cCaption);
            e.Graphics.FillRectangle(Brushes.DarkBlue, rc);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_NCHITTEST)
            {
                var pos = new Point(m.LParam.ToInt32());
                pos = this.PointToClient(pos);
                if (pos.X <= cGrip && pos.Y <= cGrip)
                {
                    m.Result = (IntPtr) HTTOPLEFT;
                    return;
                }
                else if (pos.X >= ClientSize.Width - cGrip && pos.Y >= ClientSize.Height - cGrip)
                {
                    m.Result = (IntPtr) HTBOTTOMRIGHT;
                    return;
                }

                //if (pos.Y < cCaption)
                //{
                //    m.Result = (IntPtr)2;  // HTCAPTION
                //    return;
                //}
                //if ((pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip)
                //    || (pos.X <= cGrip && pos.Y >= this.ClientSize.Height - cGrip)
                //    || (pos.X <= cGrip && pos.Y <= cGrip)
                //    || (pos.X >= this.ClientSize.Width - cGrip && pos.Y <= cGrip))
                //{
                //    m.Result = (IntPtr)17; // HTBOTTOMRIGHT
                //    return;
                //}
            }
            base.WndProc(ref m);
        }
    }
}