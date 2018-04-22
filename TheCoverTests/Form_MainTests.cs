using System;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TheCover.Tests
{
    [TestClass()]
    public class Form_MainTests
    {
        public const int WM_NCHITTEST = 0x84;
        public const int HTBOTTOMRIGHT = 17;

        [TestMethod()]
        public void WndProcTest_WhenTheMouseDownOnTheLeftTopCorner_Return_HTBOTTOMRIGHT()
        {
            var target = new Form_MainStub();
            var xPoint = 3;
            var yPoint = 3;
            var mouseMsg = new Message
            {
                Msg = WM_NCHITTEST,
                LParam = (IntPtr)((xPoint << 32) | yPoint),
            };
            mouseMsg.Msg = WM_NCHITTEST;

            target.WncProcForUnitTest(ref mouseMsg);


            Assert.AreEqual(HTBOTTOMRIGHT, mouseMsg.Result);
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