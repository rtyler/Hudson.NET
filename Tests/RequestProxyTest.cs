using System;

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
			Console.WriteLine(req.Execute("/api/json"));
		}	
	}
}
