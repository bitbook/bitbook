using System;
using System.Security.Cryptography.X509Certificates;
using System.Net.NetworkInformation;

namespace DHT.Messages
{
	public class PutRequest : Message
	{
		public bool completed {
			get;
			set;
		}
		public PutRequest ()
		{
		}
	}
}
