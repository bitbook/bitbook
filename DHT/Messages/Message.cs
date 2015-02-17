using System;
using DHT.Nodes;
using System.Configuration;

namespace DHT.Messages
{
	public class Message
	{
		public int Id { get; set; }
		public int Port { get; set; }
		public string Type { get; set; }
		public string MessageT { get; set; }
	}
}

