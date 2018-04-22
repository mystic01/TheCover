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
        public const int HTCAPTION = 2;
        public const int HTLEFT = 10;
        public const int HTRIGHT = 11;
        public const int HTTOP = 12;
        public const int HTTOPLEFT = 13;
        public const int HTTOPRIGHT = 14;
        public const int HTBOTTOM = 15;
        public const int HTBOTTOMLEFT = 16;
        public const int HTBOTTOMRIGHT = 17;

        protected override void OnPaint(PaintEventArgs e)
        {
            //var rc = new Rectangle(this.ClientSize.Width - cGrip, this.ClientSize.Height - cGrip, cGrip, cGrip);
            //ControlPaint.DrawSizeGrip(e.Graphics, this.BackColor, rc);
            //rc = new Rectangle(0, 0, this.ClientSize.Width, cCaption);
            //e.Graphics.FillRectangle(Brushes.DarkBlue, rc);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_NCHITTEST)
            {
                var clientWidth = ClientSize.Width;
                var clientHeight = ClientSize.Height;

                var pos = new Point(m.LParam.ToInt32());
                pos = PointToClient(pos);

                if (IsDownOnOverridePosition(ref m, pos, clientWidth, clientHeight))
                    return;
            }
            base.WndProc(ref m);
        }

        private bool IsDownOnOverridePosition(ref Message m, Point pos, int clientWidth, int clientHeight)
        {
            if (IsOnTheTop(pos))
            {
                if (IsOnTheLeft(pos))
                {
                    m.Result = (IntPtr)HTTOPLEFT;
                    return true;
                }
                else if (IsOnTheRight(pos, clientWidth))
                {
                    m.Result = (IntPtr)HTTOPRIGHT;
                    return true;
                }
                else
                {
                    m.Result = (IntPtr)HTTOP;
                    return true;
                }
            }
            else if (IsOnTheBottom(pos, clientHeight))
            {
                if (IsOnTheLeft(pos))
                {
                    m.Result = (IntPtr)HTBOTTOMLEFT;
                    return true;
                }
                else if (IsOnTheRight(pos, clientWidth))
                {
                    m.Result = (IntPtr)HTBOTTOMRIGHT;
                    return true;
                }
                else
                {
                    m.Result = (IntPtr)HTBOTTOM;
                    return true;
                }
            }
            else if (IsOnTheLeft(pos))
            {
                m.Result = (IntPtr)HTLEFT;
                return true;
            }
            else if (IsOnTheRight(pos, clientWidth))
            {
                m.Result = (IntPtr)HTRIGHT;
                return true;
            }
            else if (pos.X <= clientWidth && pos.Y <= clientHeight)
            {
                m.Result = (IntPtr) HTCAPTION;
                return true;
            }
            return false;
        }

        private static bool IsOnTheRight(Point pos, int clientWidth)
        {
            return pos.X >= clientWidth - cGrip;
        }

        private static bool IsOnTheBottom(Point pos, int clientHeight)
        {
            return pos.Y >= clientHeight - cGrip;
        }

        private static bool IsOnTheTop(Point pos)
        {
            return pos.Y <= cGrip;
        }

        private static bool IsOnTheLeft(Point pos)
        {
            return pos.X <= cGrip;
        }
    }
}