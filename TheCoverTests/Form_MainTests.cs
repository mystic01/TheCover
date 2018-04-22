using System;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TheCover.Tests
{
    [TestClass()]
    public class Form_MainTests
    {
        [TestMethod()]
        public void WndProcTest_WhenTheMouseDownOnTheLeftTopCorner_Return_HTBOTTOMRIGHT()
        {
            var target = new Form_MainStub();
            target.StartPosition = FormStartPosition.Manual;
            var xPoint = 3;
            var yPoint = 3;
            var mouseMsg = new Message
            {
                Msg = Form_Main.WM_NCHITTEST,
                LParam = (IntPtr)((xPoint << 16) | yPoint),
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