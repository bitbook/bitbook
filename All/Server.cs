using System;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using All.Messages;
using Newtonsoft.Json;
using Microsoft.CSharp;

namespace All
{
	public class Server
	{
		public Server (int port)
		{
			//Trigger the method PrintIncomingMessage when a packet of type 'Message' is received
			//We expect the incoming object to be a string which we state explicitly by using <string>
			NetworkComms.AppendGlobalIncomingPacketHandler<string>("Message", PrintIncomingMessage);
			//Start listening for incoming connections
			Connection.StartListening(ConnectionType.TCP, new System.Net.IPEndPoint(System.Net.IPAddress.Any, port));

			//Print out the IPs and ports we are now listening on
			Console.WriteLine("Server listening for TCP connection on:"+port);
		}
		public delegate void MyEventHandler(object source, MyEventArgs e);
		public event MyEventHandler OnMessage;

		/// <summary>
		/// Writes the provided message to the console window
		/// </summary>
		/// <param name="header">The packet header associated with the incoming message</param>
		/// <param name="connection">The connection used by the incoming message</param>
		/// <param name="message">The message to be printed to the console</param>
		private void PrintIncomingMessage(PacketHeader header, Connection connection, string message)
		{
			Message m;
			m = JsonConvert.DeserializeObject<Message> (message);
			OnMessage (this, new MyEventArgs (m));

			Console.WriteLine("\nA message was received from " + connection.ToString() + " which said '" + message + "'.");
		}
	}
}

