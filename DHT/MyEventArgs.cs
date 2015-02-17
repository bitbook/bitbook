using System;
using DHT.Messages;

namespace DHT
{
	public class MyEventArgs : EventArgs
	{
		private Message Message;

		public MyEventArgs(Message Text) {
			Message = Text;
		}

		public Message Get() {
			return Message;
		}
	}
}

