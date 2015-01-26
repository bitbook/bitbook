using System;
using NUnit.Framework;
using System.Net;
using System.Net.Sockets;
using Fleck;
using Moq;
using BitBook.WebSocketHost;

namespace BitBook.Tests
{
	[TestFixture]
	public class WebSocketServerTests
	{
		private WebSocketHost.WebSocketHost _server;
		private MockRepository _repository;

		private IPAddress _ipV4Address;
		private IPAddress _ipV6Address;

		private Socket _ipV4Socket;
		private Socket _ipV6Socket;

		[SetUp]
		public void Setup()
		{
			_repository = new MockRepository(MockBehavior.Default);
			_server = new WebSocketHost.WebSocketHost("56003");

			_ipV4Address = IPAddress.Parse("127.0.0.1");
			_ipV6Address = IPAddress.Parse("::1");

			_ipV4Socket = new Socket(_ipV4Address.AddressFamily, SocketType.Stream, ProtocolType.IP);
			_ipV6Socket = new Socket(_ipV6Address.AddressFamily, SocketType.Stream, ProtocolType.IP);
		}

		[Test]
		public void ShouldStart()
		{
			var socketMock = _repository.Create<ISocket>();

			_server.Server.ListenerSocket = socketMock.Object;
			_server.Server.Start(connection => { });

			socketMock.Verify(s => s.Bind(It.Is<IPEndPoint>(i => i.Port == 56003)));
			socketMock.Verify(s => s.Accept(It.IsAny<Action<ISocket>>(), It.IsAny<Action<Exception>>()));
		}
			
		[TearDown]
		public void TearDown()
		{
			_ipV4Socket.Dispose();
			_ipV6Socket.Dispose();
			_server.Dispose();
		}
	}
}

