using System;
using Nancy.Hosting.Self;

namespace BitBook
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Starting up BitBook!");
			Console.WriteLine ("Press enter to stop.!");
			using (var host = new NancyHost(new Uri("http://localhost:56001")))
			{
				host.Start();

				// Keep window open!
				Console.ReadLine();
				host.Stop ();
			}
		}
	}
}
