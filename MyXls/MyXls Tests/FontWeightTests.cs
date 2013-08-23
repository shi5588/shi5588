using System.Collections.Generic;
using NUnit.Framework;

namespace org.in2bits.MyXls
{
	[TestFixture]
	public class FontWeightTests
	{
		[Test]
		public void FontWeightConverterConvert()
		{
			Dictionary<ushort, FontWeight> testItems = new Dictionary<ushort, FontWeight>();
			testItems.Add(0, FontWeight.Thin);
			testItems.Add(99, FontWeight.Thin);
			testItems.Add(100, FontWeight.Thin);
			testItems.Add(449, FontWeight.Normal);
			testItems.Add(450, FontWeight.Normal);
			testItems.Add(451, FontWeight.Medium);
			testItems.Add(400, FontWeight.Normal);
			testItems.Add(900, FontWeight.Heavy);
			testItems.Add(901, FontWeight.Heavy);
			testItems.Add(1245, FontWeight.Heavy);

			foreach (KeyValuePair<ushort, FontWeight> item in testItems)
			{
				FontWeight actual = FontWeightConverter.Convert(item.Key);
				Assert.AreEqual(item.Value, actual, "Should have been {0} but converted {1} to {2}", item.Value, item.Key, actual);
			}

		}
		
	}
}
