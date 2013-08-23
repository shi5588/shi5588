using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;
using org.in2bits.MyXls.ByteUtil;

namespace org.in2bits
{
    [TestFixture]
    public class BytesTests
    {
        private byte[] sixteenBytes = { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        [Test]
        public void Instantiate()
        {
            Bytes bytes = new Bytes();
            Assert.IsTrue(bytes.IsEmpty);
            Assert.IsFalse(bytes.IsArray);
            Assert.IsNull(bytes._bytesList);
        }

        [Test]
        public void ConvertArrayToList()
        {
            Bytes bytes = new Bytes(new byte[] { sixteenBytes[0], sixteenBytes[1] });
            Assert.IsTrue(bytes.IsArray);
            bytes.Append(new byte[] { sixteenBytes[2], sixteenBytes[3] });
            Assert.IsFalse(bytes.IsArray);
        }

        [Test]
        public void AppendBytesObject()
        {
            Bytes bytes = new Bytes(new byte[] { sixteenBytes[0], sixteenBytes[1] });
            bytes.Append(new Bytes(new byte[] { sixteenBytes[2], sixteenBytes[3] }));
            Assert.AreEqual(4, bytes.Length);
            Assert.IsFalse(bytes.IsArray);
        }

        [Test]
        public void Test_Get()
        {
            Bytes bytes = new Bytes();

            Bytes newBytes = new Bytes();
            newBytes.Append(new byte[] { sixteenBytes[0], sixteenBytes[1] });
            newBytes.Append(new byte[] { sixteenBytes[2], sixteenBytes[3] });
            bytes.Append(newBytes);

            bytes.Append(new Bytes(new byte[] { sixteenBytes[4], sixteenBytes[5] }));

            newBytes = new Bytes();
            newBytes.Append(new byte[] { sixteenBytes[6], sixteenBytes[7], sixteenBytes[8] });
            newBytes.Append(new byte[] { sixteenBytes[9] });
            bytes.Append(newBytes);

            newBytes = new Bytes();
            newBytes.Append(new Bytes(new byte[] { sixteenBytes[10] }));
            newBytes.Append(new Bytes(new byte[] { sixteenBytes[11] }));
            bytes.Append(newBytes);

            newBytes = new Bytes();
            newBytes.Append(new Bytes(new byte[] { sixteenBytes[12] }));
            newBytes.Append(new Bytes(new byte[] { sixteenBytes[13], sixteenBytes[14] }));
            newBytes.Append(new Bytes(new byte[] { sixteenBytes[15] }));
            bytes.Append(newBytes);

            AssertArraysAreEqual(bytes.ByteArray, sixteenBytes);

            for (int offset = 0; offset < 16; offset++)
            {
                for (int length = 0; length <= (16 - offset); length++)
                {
                    AssertArraysAreEqual(Bytes.MidByteArray(sixteenBytes, offset, length), bytes.Get(offset, length).ByteArray);
                }
            }
        }

        [Test]
        public void GetByteArrayMultipleTimes()
        {
            byte[] a = new byte[]{0x01, 0x02};
            Bytes bytes = new Bytes(a);
            bytes.Append(new byte[] {0x03, 0x04, 0x05, 0x06});
            bytes.Append(new byte[] {0x07});
            Assert.AreEqual(7, bytes.ByteArray.Length);
            Assert.AreEqual(7, bytes.ByteArray.Length);
        }

        [Test]
        public void TestPerformance()
        {
            TestPerformance(10, 100);
        }

        private void TestPerformance(int sets, int setLength)
        {
            byte[] set = GetByteArray(setLength);

            System.Diagnostics.Stopwatch stopwatch = new Stopwatch();

            long aTicks, bTicks;

            //test manual
            stopwatch.Start();
            byte[] aResultArray = new byte[0];
            for (int i = 0; i < sets; i++)
            {
                byte[] subResult = ManuallyAppend(set, new byte[1] { (byte)i });
                aResultArray = ManuallyAppend(subResult, aResultArray);
            }
            stopwatch.Stop();
            aTicks = stopwatch.ElapsedTicks;
            System.Diagnostics.Debug.WriteLine(string.Format("ArMan: {0} ticks", aTicks));

            //test byteswise
            stopwatch.Reset();
            stopwatch.Start();
            Bytes bResult = new Bytes();
            for (int i = 0; i < sets; i++)
            {
                Bytes subResult = new Bytes();
                subResult.Append(new byte[] { (byte)i });
                subResult.Append(set);
                bResult.Append(subResult);
            }
            byte[] bResultArray = bResult.ByteArray;
            stopwatch.Stop();
            bTicks = stopwatch.ElapsedTicks;
            System.Diagnostics.Debug.WriteLine(string.Format("Bytes: {0} ticks", bTicks));
            System.Diagnostics.Debug.WriteLine(string.Format("Bytes advantage factor: {0}", bTicks == 0 ? aTicks == bTicks ? 0 : 1 : aTicks / ((decimal)bTicks)));
            AssertArraysAreEqual(aResultArray, bResultArray);
        }

        private byte[] ManuallyAppend(byte[] array, byte[] toArray)
        {
            byte[] result = new byte[array.Length + toArray.Length];
            toArray.CopyTo(result, 0);
            array.CopyTo(result, toArray.Length);
            return result;
        }

        private byte[] GetByteArray(int length)
        {
            byte[] byteArray = new byte[length];
            for (int i = 0; i < length; i++)
                byteArray[i] = (byte)i;

            return byteArray;
        }

        private void AssertArraysAreEqual(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                Assert.Fail("Arrays not equal length");

            for (int i = 0; i < a.Length; i++)
                Assert.AreEqual(a[i], b[i], string.Format("byte at pos {0} differs", i));
        }
    }
}
