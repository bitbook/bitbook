﻿using System;
using BitBook.StaticSelfHost;
using BitBook.WebSocketHost;
namespace BitBook
{
	class MainClass
	{

		public static void Main (string[] args)
		{
			Console.WriteLine ("Starting up BitBook!");
			// Start up static web server
			using (var host = new BitBook.StaticSelfHost.StaticSelfHost ("56001"))
			using (var websocket = new BitBook.WebSocketHost.WebSocketHost ("56002")) {
				Console.WriteLine ("Server started at http://localhost:56001");
				// Keep window open!
				Console.WriteLine ("Press enter to stop.!");
				Console.ReadLine ();
			}
		}
	}
}
