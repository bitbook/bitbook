using NetworkCommsDotNet;
using DHT.Nodes;
using DHT.Messages;
using System.Net;
using System;
using Newtonsoft.Json;
using PHT;
using PHT.Tests;

namespace DHT
{
	public class DynamicHashTable : IDisposable
	{
		public RoutingTable _routingTable = null;
		public int _serverPort = 0 ;
		public KeyValueStore _pht = new KeyValueStore("Test");
		public Listener server;

		public DynamicHashTable (int port)
		{
			this._serverPort = port;

			this.server = new Listener (_serverPort);
			this.server.OnMessage += new Listener.MyEventHandler(MessageReceived);

			this._routingTable = new RoutingTable (_serverPort);
		}

		public void MessageReceived(object source, MyEventArgs e) {
			Node node;
			if (_routingTable.GetNode (e.Get ().Id) == null) {
				node = _routingTable.Add (new Node (e.Get ().Id, new IPEndPoint (IPAddress.Loopback, e.Get ().Port)));
				HandleAnnounce (e.Get ().Id);
				Console.WriteLine ("Recieved New Node ID: " + e.Get ().Id + ", Added to routing table, Auto ListPeers");
			} else {
				node = _routingTable.GetNode (e.Get ().Id);
				node.Seen ();
				if (e.Get ().Type == "Ping") {
					HandlePing (node);
				}
			}

			if (e.Get ().Type == "ListPeers") {
				HandleListPeers (e.Get ().MessageT);
			}
			if (e.Get ().Type == "Store") {
				HandleStore (e.Get ().MessageT);
				Console.WriteLine ("Store recieved");
			}
			if (e.Get ().Type == "Pong") {
				Console.WriteLine ("Pong recieved");
			}
		}

		public void BroadcastMessage(string message, string type){
			foreach (var item in _routingTable.GetAllNodes ()) {
				this.server.SendMessage (item.EndPoint.Port,new Message(){Id = _routingTable.LocalNode.Id, Port = _serverPort, MessageT = message, Type = type });
				Console.WriteLine ("Sent "+type+" Message to"+item.Id);
			}
		}

		public void ListKnownPeers(){
			foreach (var item in _routingTable.GetAllNodes ()) {
				Console.WriteLine ("ID "+item.Id+" Port:"+item.EndPoint.Port);
			}
		}

		public void HandleListPeers(string s){
			Console.WriteLine ("ListOfPeers Recieved");
			string[] ips = s.Split(';');
			foreach (string ip in ips)
				
			{
				if (ip != "") {
					string[] ipPort = ip.Split(':');
					if (!_routingTable.CheckIP (int.Parse(ipPort [1]))) {
						Console.WriteLine ("Found New");
						//this.server.SendMessage (int.Parse(ipPort [1]),new Message(){Id = _routingTable.LocalNode.Id, Port = _serverPort, MessageT = "Annonce", Type = "Announce" });
						Announce (int.Parse (ipPort [1]));
					}
				}
			}
		}

		public void Ping(int Id){
			Node node = _routingTable.GetNode (Id);
			Message message = new Message () {
				Id = _routingTable.LocalNode.Id,
				Port = _serverPort,
				MessageT = "",
				Type = "Ping"
			};
			Console.WriteLine ("Ping sent to: "+Id);
			this.server.SendMessage (node.EndPoint.Port, message);
		}

		public void HandlePing(Node node){
			this.server.SendMessage (node.EndPoint.Port, new Message () {
				Id = _routingTable.LocalNode.Id,
				Port = _serverPort,
				MessageT = "",
				Type = "Pong"
			});
			Console.WriteLine ("Pong sent to: "+node.Id);
		}

		public void Announce(int port){
			this.server.SendMessage (port,new Message(){Id = _routingTable.LocalNode.Id, Port = _serverPort, MessageT = "", Type = "Ping" });
			Console.WriteLine ("Sent Message announce to Node with port:"+port);
		}

		public void HandleAnnounce(int Id){
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
			this.server.SendMessage (node.EndPoint.Port, message);
			Console.WriteLine ("HandleAnnounce: SendingListOfPeers to " + node.EndPoint.Port);
		}

		public string Get(int key){
			var a = (TestJSONSerializedEntity) _pht.Get (key);
			return a.ToJsonSerialized ();
		}

		public void Store(int key, string value){
			var mess = key + ";" + value;
			Console.WriteLine ("Storing Data");
			BroadcastMessage (mess, "Store");
		}

		public void HandleStore(string message){
			string[] arr = message.Split (';');
			var item = new TestJSONSerializedEntity (int.Parse (arr[0]), arr[1]);
			_pht.Put (item);
		}

		#region IDisposable implementation

		public void Dispose ()
		{
			NetworkComms.Shutdown ();
		}

		#endregion
	}
	
}