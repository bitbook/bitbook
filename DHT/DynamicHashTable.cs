using NetworkCommsDotNet;
using DHT.Nodes;
using DHT.Messages;
using System.Net;
using System;
using Newtonsoft.Json;
using PHT;

namespace DHT
{
	public class DynamicHashTable : IDisposable
	{
		RoutingTable _routingTable = null;
		int _serverPort = 0 ;
		KeyValueStore _pht = new KeyValueStore("Test");
		Listener server;

		public DynamicHashTable (int port)
		{
			_serverPort = port;

			var server = new Listener (_serverPort);
			server.OnMessage += new Listener.MyEventHandler(MessageReceived);

			_routingTable = new RoutingTable (_serverPort);
		}

		public void MessageReceived(object source, MyEventArgs e) {

			if (_routingTable.GetNode (e.Get ().Id) == null) {
				_routingTable.Add (new Node (e.Get ().Id, new IPEndPoint (IPAddress.Loopback, e.Get ().Port)));
				server.SendMessage (e.Get ().Port, new Message () {
					Id = _routingTable.LocalNode.Id,
					Port = _serverPort,
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

		public void BroadcastMessage(){
			foreach (var item in _routingTable.GetAllNodes ()) {
				server.SendMessage (item.EndPoint.Port,new Message(){Id = _routingTable.LocalNode.Id, Port = _serverPort, MessageT = "TestBroadcast", Type = "Ping" });
			}
		}

		public void AddPeer (int p2)
		{
			server.SendMessage (p2,new Message(){Id = _routingTable.LocalNode.Id, Port = _serverPort, MessageT = "Hello", Type = "Ping" });
		}

		public void CheckListOfPeers(string s){
			string[] ips = s.Split(';');
			foreach (string ip in ips)
			{
				if (ip != "") {
					string[] ipPort = ip.Split(':');
					if (!_routingTable.CheckIP (int.Parse(ipPort [1]))) {
						Console.WriteLine ("Found New");
						server.SendMessage (int.Parse(ipPort [1]),new Message(){Id = _routingTable.LocalNode.Id, Port = _serverPort, MessageT = "Annonce", Type = "Ping" });
					}
				}
			}
		}

		public void SendListOfPeers(int Id){
			Node node = _routingTable.GetNode (Id);
			string listOfPeers = "";
			foreach (var item in _routingTable.GetAllNodes ()) {
				listOfPeers += item.EndPoint.Address + ":" + item.EndPoint.Port + ";";
			}
			Message message = new Message () {
				Id = _routingTable.LocalNode.Id,
				Port = _serverPort,
				MessageT = listOfPeers,
				Type = "ListPeers"
			};
			server.SendMessage (node.EndPoint.Port, message);
			Console.WriteLine ("SendListOfPeers to "+node.EndPoint.Port);
		}

		#region IDisposable implementation

		public void Dispose ()
		{
			NetworkComms.Shutdown ();
		}

		#endregion
	}
	
}