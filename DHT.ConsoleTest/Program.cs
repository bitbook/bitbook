using DHT;
using System;

namespace DHT.ConsoleTest
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Enter Server port!");
			var serverPort = int.Parse (Console.ReadLine ());
			using (var DHT = new DynamicHashTable (serverPort)) {
				Console.WriteLine ("DHT Successfully started and listening on "+serverPort);
				Console.WriteLine ("----------------------------------------------------------");
				while (true) {
					Console.WriteLine ("Enter: \n-A to add a peer, P to ping a peer\n-G to get a key, S to store a key/value\n-L to list peers known");
					Console.WriteLine ("----------------------------------------------------------");	
					switch(Console.ReadLine ().ToLower ()){
					case "a":
						Console.WriteLine ("Please enter the port number to add as a peer");
						int p2 = int.Parse (Console.ReadLine ());
						if (p2 > 1024 && p2 < 49151) {
							// Add Peer via test which then sends an announce
							DHT.Announce (p2);
							Console.WriteLine ("Peer added, announce sent!");
						} else {
							Console.WriteLine ("Port needs to be within 1024 to 49151.");
						}
							break;
					case "p":
						Console.WriteLine ("Please enter the node ID to ping the peer");
						int ping = int.Parse (Console.ReadLine ());
						DHT.Ping (ping);
						break;
						case "g":
						Console.WriteLine ("Please id value of data to get:");
						int key = int.Parse (Console.ReadLine ());
						Console.WriteLine (DHT.Get (key));
							break;
						case "s":
						Console.WriteLine ("Please an int to be used as the key to store:");
						key = int.Parse (Console.ReadLine ());
						Console.WriteLine ("Please value to store:");
						string value = Console.ReadLine ();
						DHT.Store (key,value);
							break;
					case "l":
						DHT.ListKnownPeers ();
						break;
						default:
							Console.WriteLine ("Input not detected, please try again!");
							break;
					}
				}
			}

		}
	}
}
