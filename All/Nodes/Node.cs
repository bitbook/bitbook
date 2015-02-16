using System;
using System.Net;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace All.Nodes
{
	public class Node : IComparable<Node>, IEquatable<Node>
	{
		public static readonly int MaxFailures = 4;

		IPEndPoint endpoint;
		private int id;
		int failedCount;
		DateTime lastSeen;

		public IPEndPoint EndPoint
		{
			get { return endpoint; }

		}

		public int FailedCount
		{
			get { return failedCount; }
			internal set { failedCount = value; }
		}
			
		public int Id {
			get { return this.id; }
			set{ this.id = value; }
		}

		public DateTime LastSeen
		{
			get { return lastSeen; }
			internal set { lastSeen = value; }
		}
			
		public Node(int id, IPEndPoint endpoint)
		{
			this.endpoint = endpoint;
			this.id = id;
		}

		internal void Seen()
		{
			failedCount = 0;
			lastSeen = DateTime.UtcNow;
		}

		//To order by last seen in bucket
		public int CompareTo(Node other)
		{
			if (other == null)
				return 1;

			return lastSeen.CompareTo(other.lastSeen);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as Node);
		}

		public bool Equals(Node other)
		{
			if (other == null)
				return false;

			return id.Equals(other.id);
		}

		public override int GetHashCode()
		{
			return id.GetHashCode();
		}

		public override string ToString()
		{
			return this.id.ToString ();
		}
	}
}

