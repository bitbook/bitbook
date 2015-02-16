using System;
using Interfaces;

namespace PHT.Tests
{
	public class TestJSONSerializedEntity : IJsonSerializedEntity
	{
		int _id {
			get;
			set;
		}

		string _json {
			get;
			set;
		}

		public TestJSONSerializedEntity (int Id, string Json)
		{
			_id = Id;
			_json = Json;
		}

		#region IJsonSerializedEntity implementation

		public string ToJsonSerialized ()
		{
			return _json;
		}

		public int Id {
			get {
				return _id;
			}
		}

		#endregion
	}
}

