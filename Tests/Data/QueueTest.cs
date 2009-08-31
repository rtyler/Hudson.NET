using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

using NUnit.Framework;

using Hudson.Data;

namespace Hudson.Tests.QueueTests
{
	[TestFixture]
	public class OneItemInTheQueueTests
	{
		protected readonly string queueJson = "{\"items\":[{\"actions\":[{\"causes\":[{\"shortDescription\":\"Started by user anonymous\"}]}],\"blocked\":false,\"buildable\":true,\"stuck\":false,\"task\":{\"name\":\"Sleeper\",\"url\":\"http://localhost:8080/job/Sleeper/\",\"color\":\"grey_anime\"},\"why\":\"Waiting for next available executor\",\"buildableStartMilliseconds\":1251693111920}]}";
		private JavaScriptSerializer json = null;

		[SetUp]
		public void SetUp()
		{
			this.json = new JavaScriptSerializer();
		}

		[Test]
		public void BasicDeserialization()
		{
			Queue queue = this.json.Deserialize<Queue>(this.queueJson);
			Assert.IsNotNull(queue, "Deserialized Queue object is null");
		}
	}
}
