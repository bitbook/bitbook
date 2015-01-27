using System;
using Fleck;

namespace BitBook
{
	class MainClass
	{

		public static void Main (string[] args)
		{
			FleckLog.Info ("Starting up BitBook!");
			// Start up static web server
			using (var host = new StaticSelfHost.StaticSelfHost ("56001"))
			using (var websocket = new WebSocketHost.WebSocketHost ("56002")) {
				FleckLog.Info ("Server started at http://localhost:56001");
				// Keep window open!
				FleckLog.Info ("Press enter to stop.!");
				Console.ReadLine ();
			}

		}
	}
}
