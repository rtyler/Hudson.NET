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
			Dictionary<string, object> response = req.Execute("/api/json");

			Assert.IsNotNull(response);
			Assert.IsTrue(response.ContainsKey("views"));
			Assert.IsTrue(response.ContainsKey("jobs"));
		}	
	}
}
