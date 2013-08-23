using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace org.in2bits.MyXls
{
    [TestFixture]
    public class RIDTests
    {
        [Test]
        public void GetNameFromByteArray()
        {
            byte[] tableOpDifferentInstance = new byte[] { 0x36, 0x02 };
            byte[] tableOp = RID.TABLEOP;
            Assert.AreEqual("TABLEOP", RID.Name(tableOp), "RID name");
            Assert.AreEqual("TABLEOP", RID.Name(tableOpDifferentInstance), "RID name");
        }
    }
}
