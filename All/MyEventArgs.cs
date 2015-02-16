using System;
using All.Messages;

namespace All
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

