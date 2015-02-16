using System;
using NetworkCommsDotNet;
using All.Nodes;
using System.Collections.Generic;
using All.Messages;
using System.Net;
using Newtonsoft.Json;

namespace All
{
	class MainClass
	{
		public static RoutingTable _routingTable = null;
		public static int serverPort = 0 ;

		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello World!");
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
			NetworkComms.Shutdown ();
		}

		static void MessageReceived(object source, MyEventArgs e) {

			if (_routingTable.GetNode (e.Get ().Id) == null) {
				_routingTable.Add (new Node (e.Get ().Id, new IPEndPoint (IPAddress.Loopback, e.Get ().Port)));
				SendMessage (e.Get ().Port, new Message () {
					Id = _routingTable.LocalNode.Id,
					Port = serverPort,
					MessageT = "Annonce",
					Type = "Ping"
				});
				Console.WriteLine ("New Node Send Annonce");
			} else {
				_routingTable.GetNode (e.Get ().Id).Seen ();
			}
			if (e.Get ().Type == "GetPeers") {
				SendListOfPeers (e.Get ().Id);
			}
			if (e.Get ().Type == "ListPeers") {
				CheckListOfPeers (e.Get ().MessageT);
			}
			Console.WriteLine("Recieved Message of Type : "+e.Get ().Type);
		}

		static void BroadcastMessage(){
			foreach (var item in _routingTable.GetAllNodes ()) {
				SendMessage (item.EndPoint.Port,new Message(){Id = _routingTable.LocalNode.Id, Port = serverPort, MessageT = "TestBroadcast", Type = "Ping" });
			}
		}

		static void SendMessage(int port, Message message){
			NetworkComms.SendObject("Message", "127.0.0.1", port, JsonConvert.SerializeObject (message));
			Console.WriteLine ("SentMessage to "+port);
		}

		static void CheckListOfPeers(string s){
			string[] ips = s.Split(';');
			foreach (string ip in ips)
			{
				if (ip != "") {
					string[] ipPort = ip.Split(':');
					if (!_routingTable.CheckIP (int.Parse(ipPort [1]))) {
						Console.WriteLine ("Found New");
						SendMessage (int.Parse(ipPort [1]),new Message(){Id = _routingTable.LocalNode.Id, Port = serverPort, MessageT = "Annonce", Type = "Ping" });
					}
				}
			}
		}

		static void SendListOfPeers(int Id){
			Node node = _routingTable.GetNode (Id);
			string listOfPeers = "";
			foreach (var item in _routingTable.GetAllNodes ()) {
				listOfPeers += item.EndPoint.Address + ":" + item.EndPoint.Port + ";";
			}
			Message message = new Message () {
				Id = _routingTable.LocalNode.Id,
				Port = serverPort,
				MessageT = listOfPeers,
				Type = "ListPeers"
			};
			SendMessage (node.EndPoint.Port, message);
			Console.WriteLine ("SendListOfPeers to "+node.EndPoint.Port);
		}
	}
	
}