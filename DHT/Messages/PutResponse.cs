using System;

namespace DHT.Messages
{
	public class PutResponse : Message
	{
		public bool completed {
			get;
			set;
		}
		public PutResponse ()
		{
		}
	}
}
