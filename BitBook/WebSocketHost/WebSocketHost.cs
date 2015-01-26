using System;
using Fleck;
namespace BitBook.WebSocketHost
{
	public class WebSocketHost : IDisposable
	{
		public WebSocketHost (string Port)
		{

		}
		public void Dispose(){
			host.Dispose ();
			GC.SuppressFinalize(this);
		}
	}
}

