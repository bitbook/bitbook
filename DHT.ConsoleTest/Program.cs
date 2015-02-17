using DHT;
using System;

namespace DHT.IntergrationTest
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Enter Server port!");
			var serverPort = int.Parse (Console.ReadLine ());
			using (var DHT = new DynamicHashTable (serverPort)) {
				while (true) {
					Console.WriteLine ("Enter port to send message too:");
					int p2 = int.Parse (Console.ReadLine ());
					if (p2 < 1000) {
						if (p2 == 0)
							break;
						if (p2 == 1) {
							DHT.BroadcastMessage ();
						}
						else {
						}
					}
					else {
						// Add Peer via test which then sends an announce b
						DHT.AddPeer (p2);
					}
				}
			}

		}
	}
}
