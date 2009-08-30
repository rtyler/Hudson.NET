using System;
using System.Collections.Generic;

using NUnit.Framework;

using Hudson.Internal;

namespace Hudson.Tests
{
	[TestFixture]
	public class RequestProxyTest
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
		public void ExecuteSimpleRequest()
		{
			RequestProxy req = new RequestProxy("localhost", 8888);

			Assert.IsNotNull(req, "RequestProxy object is null");

			Dictionary<string, object> response = req.Execute("/api/json");

			Assert.IsNotNull(response, "Response from RequestProxy.Execute is null");

			Assert.IsTrue(response.ContainsKey("views"), "Response doesn't contain a 'views'");
			Assert.IsTrue(response.ContainsKey("jobs"), "Response doesn't contain 'jobs'");
		}	
	}
}
