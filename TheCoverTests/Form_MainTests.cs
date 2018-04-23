using System;
using System.Drawing;
using System.Windows.Forms;
using NUnit.Framework;

namespace TheCover.Tests
{
    [TestFixture]
    public class Form_MainTests
    {
        [Test]
        [TestCase(3, 3, Form_Main.HTTOPLEFT, TestName = "WndProcTest_MouseDownOnTopLeft_ReturnHTTOPLEFT")]
        [TestCase(97, 197, Form_Main.HTBOTTOMRIGHT, TestName = "WndProcTest_MouseDownOnBottomRight_ReturnHTBOTTOMRIGHT")]
        [TestCase(97, 3, Form_Main.HTTOPRIGHT, TestName = "WndProcTest_MouseDownOnTopRight_ReturnHTTOPRIGHT")]
        [TestCase(3, 197, Form_Main.HTBOTTOMLEFT, TestName = "WndProcTest_MouseDownOnBottomLeft_ReturnHTBOTTOMLEFT")]
        [TestCase(50, 3, Form_Main.HTTOP, TestName = "WndProcTest_MouseDownOnTop_ReturnHTTOP")]
        [TestCase(50, 197, Form_Main.HTBOTTOM, TestName = "WndProcTest_MouseDownOnBottom_ReturnHTBOTTOM")]
        [TestCase(3, 50, Form_Main.HTLEFT, TestName = "WndProcTest_MouseDownOnLeft_ReturnHTLEFT")]
        [TestCase(97, 50, Form_Main.HTRIGHT, TestName = "WndProcTest_MouseDownOnRight_ReturnHTRIGHT")]
        public void WndProcTest_WhenTheMouseDownOnSomewhere_Return_HT(int xPoint, int yPoint,
            int assertResult)
        {
            var target = new Form_MainStub();
            target.ClientSize = new Size(100, 200);
            target.StartPosition = FormStartPosition.Manual;
            var mouseMsg = new Message
            {
                Msg = Form_Main.WM_NCHITTEST,
                LParam = (IntPtr)(xPoint | (yPoint << 16)),
            };
            mouseMsg.Msg = Form_Main.WM_NCHITTEST;

            target.WncProcForUnitTest(ref mouseMsg);

            Assert.AreEqual(assertResult, (int)mouseMsg.Result);
        }
    }

    public class Form_MainStub : Form_Main
    {
        public void WncProcForUnitTest(ref Message msg)
        {
            WndProc(ref msg);
        }
    }
}