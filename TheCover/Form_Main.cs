﻿using System;
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
        public static readonly int WM_LBUTTONDOWN = 0x0201;
        public const int HTCAPTION = 2;
        public const int HTLEFT = 10;
        public const int HTRIGHT = 11;
        public const int HTTOP = 12;
        public const int HTTOPLEFT = 13;
        public const int HTTOPRIGHT = 14;
        public const int HTBOTTOM = 15;
        public const int HTBOTTOMLEFT = 16;
        public const int HTBOTTOMRIGHT = 17;

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

        private readonly SetupIniIP _setupIni = new SetupIniIP();
        private readonly string _setupIniName = "setting.ini";
        private ContextMenu _contextMenu = new ContextMenu();

        private void Form_Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveToIniFile();
        }

        private void SaveToIniFile()
        {
            _setupIni.IniWriteValue("Position", "Top", Top.ToString(), _setupIniName);
            _setupIni.IniWriteValue("Position", "Left", Left.ToString(), _setupIniName);
            _setupIni.IniWriteValue("Size", "Width", ClientSize.Width.ToString(), _setupIniName);
            _setupIni.IniWriteValue("Size", "Height", ClientSize.Height.ToString(), _setupIniName);
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
            SetupByIniFile();

            var exitMenuItem = _contextMenu.MenuItems.Add("Exit");
            exitMenuItem.Click += ExitMenuItem_Click;
            ContextMenu = _contextMenu;
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SetupByIniFile()
        {
            var positionTop = _setupIni.IniReadValue("Position", "Top", _setupIniName);
            var positionLeft = _setupIni.IniReadValue("Position", "Left", _setupIniName);
            var sizeWidth = _setupIni.IniReadValue("Size", "Width", _setupIniName);
            var sizeHeight = _setupIni.IniReadValue("Size", "Height", _setupIniName);

            try
            {
                Top = int.Parse(positionTop);
                Left = int.Parse(positionLeft);
                ClientSize = new Size(int.Parse(sizeWidth), int.Parse(sizeHeight));
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Form_Main_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}