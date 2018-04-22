using System;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TheCover.Tests
{
    [TestClass()]
    public class Form_MainTests
    {
        [TestMethod()]
        public void WndProcTest_WhenTheMouseDownOnTheLeftTopCorner_Return_HTTOPLEFT()
        {
            var target = new Form_MainStub();
            target.StartPosition = FormStartPosition.Manual;
            var xPoint = 3;
            var yPoint = 3;
            var mouseMsg = new Message
            {
                Msg = Form_Main.WM_NCHITTEST,
                LParam = (IntPtr)(xPoint | (yPoint << 16)),
            };
            mouseMsg.Msg = Form_Main.WM_NCHITTEST;

            target.WncProcForUnitTest(ref mouseMsg);

            Assert.AreEqual(Form_Main.HTTOPLEFT, (int)mouseMsg.Result);
        }

        [TestMethod()]
        public void WndProcTest_WhenTheMouseDownOnTheRightBottomCorner_Return_HTBOTTOMRIGHT()
        {
            var target = new Form_MainStub();
            target.StartPosition = FormStartPosition.Manual;
            var xPoint = target.Width - 3;
            var yPoint = target.Height - 3;
            var mouseMsg = new Message
            {
                Msg = Form_Main.WM_NCHITTEST,
                LParam = (IntPtr)(xPoint | (yPoint << 16)),
            };
            mouseMsg.Msg = Form_Main.WM_NCHITTEST;

            target.WncProcForUnitTest(ref mouseMsg);

            Assert.AreEqual(Form_Main.HTBOTTOMRIGHT, (int)mouseMsg.Result);
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