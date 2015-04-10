using System;
using Fleck;

namespace BitBook.WebSocketHost
{
	public class WebSocketHost : IDisposable
	{
		private bool disposed = false;
		// to detect redundant calls
		public WebSocketServer Server { get; set; }

		public WebSocketHost (string port)
		{
			Server = new WebSocketServer ("ws://0.0.0.0:" + port);
			Server.Start (socket => {
				socket.OnOpen = () => Console.WriteLine ("New WebsocketOpen!");
				socket.OnClose = () => Console.WriteLine ("WebsocketOpen Close!");
				socket.OnMessage = message => socket.Send (message);
			});
		}

		public void Dispose ()
		{
			Dispose (true);
			GC.SuppressFinalize (this);
		}

		protected virtual void Dispose (bool disposing)
		{
			if (!disposed) {
				if (disposing) {
					if (Server != null) {
						Server.Dispose ();
					}
				}

				disposed = true;
			}
		}
	}
}

