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
        public const int HTTOPLEFT = 13;
        public const int HTTOPRIGHT = 14;
        public const int HTBOTTOMLEFT = 16;
        public const int HTBOTTOMRIGHT = 17;

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
                var clientWidth = ClientSize.Width;
                var clientHeight = ClientSize.Height;

                var pos = new Point(m.LParam.ToInt32());
                pos = PointToClient(pos);

                if (pos.X <= cGrip && pos.Y <= cGrip)
                {
                    m.Result = (IntPtr)HTTOPLEFT;
                    return;
                }
                else if (pos.X >= clientWidth - cGrip && pos.Y >= clientHeight - cGrip)
                {
                    m.Result = (IntPtr)HTBOTTOMRIGHT;
                    return;
                }
                else if (pos.X >= clientWidth - cGrip && pos.Y <= cGrip)
                {
                    m.Result = (IntPtr)HTTOPRIGHT;
                    return;
                }
                else if (pos.X <= cGrip && pos.Y >= clientHeight - cGrip)
                {
                    m.Result = (IntPtr)HTBOTTOMLEFT;
                    return;
                }
            }
            base.WndProc(ref m);
        }
    }
}