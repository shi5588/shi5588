using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace MyXls.SL2.Tests
{
    [TestFixture]
    public class SortedListTests
    {
        [Test]
        public void Instantiate()
        {
            var sl = new org.in2bits.MyXls.SortedList<int, string>();
        }

        [Test]
        public void AddItem()
        {
            var sl = new org.in2bits.MyXls.SortedList<int, string>();
            sl.Add(2, "hello");
            Assert.AreEqual(1, sl.Count, "Count");
        }

        [Test]
        public void AddTwoItems()
        {
            var sl = new org.in2bits.MyXls.SortedList<int, string>();
            sl.Add(1, "hello");
            sl.Add(2, "world");
            Assert.AreEqual(2, sl.Count, "Count");
            var iterations = 0;
            foreach (var pair in sl)
            {
                iterations++;
            }
            Assert.AreEqual(2, iterations, "Enumerated item count");
        }

        [Test]
        public void AddTwoItemsInReverseOrder()
        {
            var sl = new org.in2bits.MyXls.SortedList<int, string>();
            sl.Add(3, "world");
            sl.Add(1, "hello");
            Assert.AreEqual(2, sl.Count, "Count");
            Assert.IsFalse(sl.ContainsKey(0), "ContainsKey 0");
            var slArray = new KeyValuePair<int, string>[sl.Count];
            sl.CopyTo(slArray, 0);
            Assert.IsNotNull(slArray[0], "Copied array element 0");
            Assert.IsNotNull(slArray[1], "Copied array element 1");
            Assert.AreEqual(1, slArray[0].Key, "Zeroth key");
            Assert.AreEqual("hello", slArray[0].Value, "0th value");
            Assert.AreEqual(3, slArray[1].Key, "1th key");
            Assert.AreEqual("world", slArray[1].Value, "1th value");
        }

        [Test]
        public void AddAndRemoveItem()
        {
            var sl = new org.in2bits.MyXls.SortedList<int, string>();
            sl.Add(3, "world");
            Assert.AreEqual(1, sl.Count, "List count before removal");
            sl.Remove(3);
            Assert.AreEqual(0, sl.Count, "List count after removal");
        }

        [Test]
        public void AddTwoItemsAndRemoveOne()
        {
            var sl = new org.in2bits.MyXls.SortedList<int, string>();
            sl.Add(3, "world");
            sl.Add(1, "hello");
            Assert.AreEqual(2, sl.Count, "List count before removal");
            sl.Remove(3);
            Assert.AreEqual(1, sl.Count, "List count after removal");
            Assert.IsTrue(sl.ContainsKey(1), "List contains key 1 after removal");
            Assert.IsFalse(sl.ContainsKey(3), "List contains key 3 after removal");
        }

        [Test]
        public void AddTwoItemsAndClear()
        {
            var sl = new org.in2bits.MyXls.SortedList<int, string>();
            sl.Add(3, "world");
            sl.Add(1, "hello");
            Assert.AreEqual(2, sl.Count, "List count before clear");
            Assert.AreEqual("hello", sl[1]);
            sl.Clear();
            Assert.AreEqual(0, sl.Count);
        }

        [Test]
        public void AddTwoItemsClearReAddOneWithDifferentKey()
        {
            var sl = new org.in2bits.MyXls.SortedList<int, string>();
            sl.Add(3, "world");
            sl.Add(1, "hello");
            Assert.AreEqual(2, sl.Count, "List count before clear");
            Assert.AreEqual("hello", sl[1]);
            sl.Clear();
            Assert.AreEqual(0, sl.Count);
            sl.Add(3, "hello");
            Assert.IsFalse(sl.ContainsKey(1), "List item 3 after clear and add");
            Assert.AreEqual("hello", sl[3], "List item 3 value after re-add");
        }
    }
}
