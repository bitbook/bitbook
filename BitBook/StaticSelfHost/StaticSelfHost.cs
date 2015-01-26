using System;
using Nancy.Hosting.Self;

namespace BitBook.StaticSelfHost
{
	public class StaticSelfHost : IDisposable
	{
		bool disposed = false; // to detect redundant calls
		NancyHost host{ get; set;}

		public StaticSelfHost (string port)
		{
			host = new NancyHost (new Uri ("http://localhost:" + port));
			host.Start();
		}

		public void Dispose(){
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					if (host != null)
					{
						host.Dispose ();
					}
				}

				disposed = true;
			}
		}
	}
}
