using NUnit.Framework;
using org.in2bits.MyXls.ByteUtil;

namespace org.in2bits
{
    [TestFixture]
    public class BitsTests
    {
        [Test]
        public void LittleEndian0x01()
        {
            byte b = 0x01;
            Bytes bytes = new Bytes();
            bytes.Append(b);
            bool[] bits = bytes.GetBits().Values;
            Assert.AreEqual(8, bits.Length, "Bits length");
            Assert.IsTrue(bits[0], "Bit 0 value");
            for (int i = 1; i < 8; i++)
                Assert.IsFalse(bits[i], string.Format("Bit {0} value", i));
        }

//        [Test]
//        public void BigEndian0x01()
//        {
//            byte b = 0x01;
//            Bytes bytes = new Bytes();
//            bytes.Append(b);
//            bool[] bits = bytes.GetBits(true).Values;
//            Assert.AreEqual(8, bits.Length, "Bits length");
//            for (int i = 0; i < 7; i++)
//                Assert.IsFalse(bits[i], string.Format("Bit {0} value", i));
//            Assert.IsTrue(bits[7], "Bit 7 value");
//        }

        [Test]
        public void LittleEndian0xFE()
        {
            byte b = 0xFE;
            Bytes bytes = new Bytes();
            bytes.Append(b);
            bool[] bits = bytes.GetBits().Values;
            Assert.AreEqual(8, bits.Length, "Bits length");
            Assert.IsFalse(bits[0], "Bit 0 value");
            for (int i = 1; i < 8; i++)
                Assert.IsTrue(bits[i], string.Format("Bit {0} value", i));
        }

//        [Test]
//        public void BigEndian0xFE()
//        {
//            byte b = 0xFE;
//            Bytes bytes = new Bytes();
//            bytes.Append(b);
//            bool[] bits = bytes.GetBits(true).Values;
//            Assert.AreEqual(8, bits.Length, "Bits length");
//            for (int i = 0; i < 7; i++)
//                Assert.IsTrue(bits[i], string.Format("Bit {0} value", i));
//            Assert.IsFalse(bits[7], "Bit 7 value");
//        }

        [Test]
        public void LittleEndian0x0101()
        {
            byte[] bs = new byte[] { 0x01, 0x01 };
            Bytes bytes = new Bytes();
            bytes.Append(bs);
            bool[] bits = bytes.GetBits().Values;
            Assert.AreEqual(16, bits.Length, "Bits length");
            Assert.IsTrue(bits[0], "Bit 0 value");
            Assert.IsTrue(bits[8], "Bit 8 value");
            for (int i = 1; i < 8; i++)
                Assert.IsFalse(bits[i], string.Format("Bit {0} value", i));
            for (int i = 9; i < 16; i++)
                Assert.IsFalse(bits[i], string.Format("Bit {0} value", i));
        }

//        [Test]
//        public void BigEndian0x0101()
//        {
//            byte[] bs = new byte[] { 0x01, 0x01 };
//            Bytes bytes = new Bytes();
//            bytes.Append(bs);
//            bool[] bits = bytes.GetBits(true).Values;
//            Assert.AreEqual(16, bits.Length, "Bits length");
//            Assert.IsTrue(bits[7], "Bit 7 value");
//            Assert.IsTrue(bits[15], "Bit 15 value");
//            for (int i = 0; i < 7; i++)
//                Assert.IsFalse(bits[i], string.Format("Bit {0} value", i));
//            for (int i = 8; i < 15; i++)
//                Assert.IsFalse(bits[i], string.Format("Bit {0} value", i));
//        }
    }
}
