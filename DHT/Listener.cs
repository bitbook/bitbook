using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using Newtonsoft.Json;
using DHT.Messages;
using System.Net;
using System;

namespace DHT
{
	public class Listener
	{
		public Listener (int port)
		{
			//Trigger the method PrintIncomingMessage when a packet of type 'Message' is received
			//We expect the incoming object to be a string which we state explicitly by using <string>
			NetworkComms.AppendGlobalIncomingPacketHandler<string>("Message", PrintIncomingMessage);
			//Start listening for incoming connections
			Connection.StartListening(ConnectionType.TCP, new IPEndPoint(IPAddress.Any, port));
		}

		public void SendMessage(int port, Message message){
			var mess =  JsonConvert.SerializeObject (message);
			NetworkComms.SendObject("Message", "127.0.0.1", port, mess);
			//Console.WriteLine ("SentMessage to "+mess);
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

			//Console.WriteLine("\nA message was received from " + connection.ToString() + " which said '" + message + "'.");
		}
	}
}

