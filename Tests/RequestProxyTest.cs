using System;
using System.Collections.Generic;
using System.Net;

using NUnit.Framework;

using Hudson.Internal;

namespace Hudson.Tests.RequestProxyTests
{
	[TestFixture]
	public class ConstructorsTests
	{
		[Test]
		[ExpectedException(typeof(InvalidRequestException))]
		public void ZeroPortRequestProxy()
		{
			RequestProxy req = new RequestProxy("localhost", 0);
		}

		[Test]
		[ExpectedException(typeof(InvalidRequestException))]
		public void MinusOnePortRequestProxy()
		{
			RequestProxy req = new RequestProxy("localhost", -1);
		}

		[Test]
		[ExpectedException(typeof(InvalidRequestException))]
		public void OversizedPortRequestProxy()
		{
			RequestProxy req = new RequestProxy("localhost", 65536);
		}

		[Test]
		[ExpectedException(typeof(InvalidRequestException))]
		public void BadHostNameRequestProxy()
		{
			RequestProxy req = new RequestProxy("");
		}

		[Test]
		public void DefaultPortRequestProxy()
		{
			RequestProxy req = new RequestProxy("localhost");
			Assert.AreEqual(8080, req.Port, "Default port should be 8080");
		}
	}

	[TestFixture]
	public class PropertiesTests
	{
		private RequestProxy req = null;
		[SetUp]
		public void SetUp()
		{
			this.req = new RequestProxy("localhost", 8080);
		}

		[Test]
		public void ProtocolPrefixSSL()
		{
			this.req.useSSL = true;
			Assert.AreEqual("https", this.req.ProtocolPrefix, "Invalid prefix");
		}

		[Test]
		public void ProtocolPrefixNoSSL()
		{
			this.req.useSSL = false;
			Assert.AreEqual("http", this.req.ProtocolPrefix, "Invalid prefix");
		}
	}

	[TestFixture]
	public class ExecuteTests
	{
		[Test]
		public void ExecuteToDownServer()
		{
			try
			{
				// I'm hoping 9080 isn't actually listening...
				RequestProxy req = new RequestProxy("localhost", 9080);
			}
			catch (WebException exc)
			{
				Assert.IsInstanceOfType(typeof(System.Net.Sockets.SocketException), 
						exc.InnerException, 
						"We should have a SocketException as an InnerException");
			}
		}

		[Test]
		public void ExecuteSimpleRequest()
		{
			RequestProxy req = new RequestProxy("localhost");

			Assert.IsNotNull(req, "RequestProxy object is null");

			Dictionary<string, object> response = req.Execute("/api/json");

			Assert.IsNotNull(response, "Response from RequestProxy.Execute is null");

			Assert.IsTrue(response.ContainsKey("views"), "Response doesn't contain a 'views'");
			Assert.IsTrue(response.ContainsKey("jobs"), "Response doesn't contain 'jobs'");
		}	

		[Test]
		public void GenericsExecuteJobTest()
		{
			RequestProxy req = new RequestProxy("localhost");
			Assert.IsNotNull(req, "RequestProxy object is null");
				
			Hudson.Data.Job job = req.Execute<Hudson.Data.Job>("/api/json");
			Assert.IsNotNull(job);
		}
	}
}
