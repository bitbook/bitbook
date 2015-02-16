using System;

namespace DHT.IntergrationTest
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Enter Server port!");
			serverPort = int.Parse(Console.ReadLine ());
			Server server = new Server (serverPort);
			server.OnMessage += new All.Server.MyEventHandler(MessageReceived);

			_routingTable = new RoutingTable (serverPort);

			while(true){
				Console.WriteLine ("Enter port to send message too:");
				int p2 = int.Parse(Console.ReadLine ());
				if (p2 < 1000) {
					if (p2 == 0)
						break;
					if (p2 == 1) {
						BroadcastMessage ();
					} else {

					}
				} else {
					// Add Peer via test which then sends an announce b
					SendMessage (p2, new Message (){ Id = _routingTable.LocalNode.Id, Port = serverPort, MessageT = "Test", Type = "GetPeers" });
				}
			}
		}
	}
}
