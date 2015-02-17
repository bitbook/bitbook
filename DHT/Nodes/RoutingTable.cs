using System.Collections.ObjectModel;
using System;
using System.Net;

namespace DHT.Nodes
{
	public class RoutingTable
	{
		static readonly Random random = new Random();
		private readonly Node localNode;
		private Bucket bucket = new Bucket();

		public Node LocalNode
		{
			get { return localNode; }
		}

		public RoutingTable(int port)
			: this(new Node(CreateNewID(), new IPEndPoint(IPAddress.Loopback, port)))
		{
		}

		public RoutingTable(Node localNode)
		{
			if (localNode == null)
				throw new ArgumentNullException("localNode");

			this.localNode = localNode;
			Console.WriteLine (localNode.Id);
			localNode.Seen();
			Add();
		}

		private bool Add(){
			return bucket.Add(this.localNode);
		}

		public static int CreateNewID ()
		{
			return random.Next ();
		}

		public bool Add(Node node)
		{
			if (node == null)
				throw new ArgumentNullException("node");
			
			if (bucket.Nodes.Contains(node))
				return false;

			bool added = bucket.Add(node);
			return added;
		}

		public Node GetNode(int id){
			return bucket.Get (id);
		}

		public bool CheckIP(int port){
			return bucket.GetOnIp (port) != null;
		}

		public ReadOnlyCollection<Node> GetAllNodes(){
			return new ReadOnlyCollection<Node>(bucket.Nodes);
		}

		public int CountNodes()
		{
			return bucket.Nodes.Count;
		}
		internal void Clear()
		{
			bucket.Clear();
		}
	}
}

