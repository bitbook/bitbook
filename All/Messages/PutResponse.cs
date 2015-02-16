using System;

namespace All.Messages
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
