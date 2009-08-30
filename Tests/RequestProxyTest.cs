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
		public void BaseJSONAPI()
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
