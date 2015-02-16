using System;
using System.Collections.Generic;
using System.Linq;

namespace All.Nodes
{
	/// <summary>
	/// This class holds a maximum amount of 8 Nodes and is itself a child of a RoutingTable
	/// </summary>
	internal class Bucket
	{
		List<Node> nodes = new List<Node>(50);
		Node replacement;

		public List<Node> Nodes
		{
			get { return nodes; }
		}

		internal Node Replacement
		{
			get { return replacement; }
			set { replacement = value; }
		}

		public Bucket()
		{

		}
			
		public bool Add(Node node)
		{
			// if the current bucket is not full we directly add the Node
			if (nodes.Count < 50)
			{
				nodes.Add(node);
				return true;
			}
			return false;
		}

		public Node Get(int id){
			return nodes.Find (x=>x.Id == id);
		}

		public Node GetOnIp(int port){
			return nodes.Find (x=>x.EndPoint.Port == port);
		}

		internal void SortBySeen()
		{
			nodes.Sort();
		}

		public void Clear(){
			nodes = new List<Node> (50);
		}
	}
}
	