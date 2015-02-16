using NUnit.Framework;
using System;
using PHT;
using System.Threading;

namespace PHT.Tests
{
	[TestFixture ()]
	public class Test
	{
		[Test ()]
		[ExpectedException(typeof(Exception))]
		public void TestEmptyGet ()
		{
			var pht = new KeyValueStore ("BitBook");
			pht.Get (1);
		}
		[Test ()]
		public void TestPutGet ()
		{
			var pht = new KeyValueStore ("BitBook");
			var item = new TestJSONSerializedEntity (1, "Hello");
			pht.Put (item);
			var itemRetrieved = pht.Get (item.Id);
			Assert.AreEqual (itemRetrieved, item);
		}
		[Test ()]
		[ExpectedException(typeof(Exception))]
		public void TestPutRemove ()
		{
			var pht = new KeyValueStore ("BitBook");
			var item = new TestJSONSerializedEntity (1, "Hello");
			pht.Put (item);
			pht.Remove (item);
			pht.Get (1);
		}
	}
}

