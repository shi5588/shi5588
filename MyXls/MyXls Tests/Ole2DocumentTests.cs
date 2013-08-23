using System;
using System.IO;
using NUnit.Framework;
using org.in2bits.MyOle2;
using org.in2bits.MyXls.ByteUtil;
using org.in2bits.MyXls.Tests;

namespace org.in2bits.MyOle2Tests
{
    [TestFixture]
    public class Ole2DocumentTests
    {
        [Test]
        public void TestWritingBinaryFile()
        {
            byte[] bytes = { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            WriteBytesToFile(bytes);
        }

        private static void WriteBytesToFile(byte[] bytes, string fileName)
        {
            FileInfo fi = new FileInfo(fileName);
            if (File.Exists(fi.Name))
                File.Delete(fi.Name);
            FileStream fileStream = fi.Open(FileMode.Create, FileAccess.Write, FileShare.Read);
            fileStream.Write(bytes, 0, bytes.Length);
            fileStream.Close();
            fileStream.Dispose();
        }

        private static void WriteBytesToFile(byte[] bytes)
        {
            WriteBytesToFile(bytes, "test.bin");
        }

        [Test]
        public void CompareByteArrays()
        {
            byte[] a =
                new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            byte[] b =
                new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

            Assert.IsTrue(Bytes.AreEqual(a, b));
            a[2] = 0x22;
            Assert.IsFalse(Bytes.AreEqual(a, b));
        }

        [Test]
        public void WriteBinaryInt()
        {
            long myShort = 5;

            byte[] myBytes = BitConverter.GetBytes(myShort);

            WriteBytesToFile(myBytes);
        }

        [Test]
        public void Instantiate()
        {
#pragma warning disable 168
            Ole2Document doc = new Ole2Document();
#pragma warning restore 168
        }

        [Test]
        public void TestEmptyBlankBiff8Streams()
        {
            byte[] stream1Name = new byte[]
					{	0x57, 0x00, 0x6F, 0x00, 0x72, 0x00, 0x6B, 0x00, 0x62, 0x00, 
						0x6F, 0x00, 0x6F, 0x00, 0x6B, 0x00, 0x00, 0x00};
            byte[] stream2Name = new byte[]
					{	0x05, 0x00, 0x53, 0x00, 0x75, 0x00, 0x6D, 0x00, 0x6D, 0x00, 
						0x61, 0x00, 0x72, 0x00, 0x79, 0x00, 0x49, 0x00, 0x6E, 0x00, 
						0x66, 0x00, 0x6F, 0x00, 0x72, 0x00, 0x6D, 0x00, 0x61, 0x00, 
						0x74, 0x00, 0x69, 0x00, 0x6F, 0x00, 0x6E, 0x00, 0x00, 0x00};

            byte[] stream1 = GetBytes(TestsConfig.ReferenceFileFolder + "Stream1.bin");
            Assert.AreEqual(579, stream1.Length);
            byte[] stream2 = GetBytes(TestsConfig.ReferenceFileFolder + "Stream2.bin");
            Assert.AreEqual(312, stream2.Length);

            Ole2Document doc = new Ole2Document();
            doc.Streams.AddNamed(stream1, stream1Name);
            doc.Streams.AddNamed(stream2, stream2Name);

            WriteBytesToFile(doc.Bytes.ByteArray, "test.xls");

            Assert.AreEqual(3072, doc.Bytes.Length);
        }

        [Test]
        public void TestLoad()
        {
            Ole2Document doc = new Ole2Document();
            FileInfo fi = new FileInfo(TestsConfig.ReferenceFileFolder + "Book1.xls");
            doc.Load(fi.Open(FileMode.Open, FileAccess.Read, FileShare.Read));
            Assert.AreEqual(2, doc.Streams.Count, "# Streams");
            byte[] refStream1 = GetBytes(TestsConfig.ReferenceFileFolder + "Stream1.bin");
            byte[] refStream2 = GetBytes(TestsConfig.ReferenceFileFolder + "Stream2.bin");
            byte[] tstStream1 = doc.Streams[1].Bytes.ByteArray;
            byte[] tstStream2 = doc.Streams[2].Bytes.ByteArray;
            Assert.AreEqual(refStream1, tstStream1, "Stream 1 ref & test stream bytes");
            Assert.AreEqual(refStream2, tstStream2, "Stream 2 ref & test stream bytes");
        }

        private static byte[] GetBytes(string fileName)
        {
            FileInfo fi = new FileInfo(fileName);
            FileStream fs = fi.OpenRead();
            byte[] byteArray = new byte[fs.Length];
            fs.Read(byteArray, 0, byteArray.Length);
            fs.Close();
            return byteArray;
        }
    }
}
