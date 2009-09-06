using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

using NUnit.Framework;

using Hudson.Data;

namespace Hudson.Tests.RootTests
{
	[TestFixture]
	public class RootTest : Hudson.Tests.TestBase
	{
		protected readonly string sampleRoot = "{\"assignedLabels\":[{}],\"mode\":\"NORMAL\",\"nodeDescription\":\"the master Hudson node\",\"nodeName\":\"\",\"numExecutors\":2,\"description\":null,\"jobs\":[{\"name\":\"Downstream\",\"url\":\"http://localhost:8080/job/Downstream/\",\"color\":\"blue\"},{\"name\":\"Hudson.NET\",\"url\":\"http://localhost:8080/job/Hudson.NET/\",\"color\":\"red\"},{\"name\":\"Sleeper\",\"url\":\"http://localhost:8080/job/Sleeper/\",\"color\":\"blue\"}],\"primaryView\":{\"name\":\"All\",\"url\":\"http://localhost:8080/\"},\"slaveAgentPort\":0,\"useCrumbs\":false,\"useSecurity\":false,\"views\":[{\"name\":\"All\",\"url\":\"http://localhost:8080/\"},{\"name\":\"TestView\",\"url\":\"http://localhost:8080/view/TestView/\"}]}";

		[Test]
		public void BasicDeserialization()
		{
			Root root = this.json.Deserialize<Root>(this.sampleRoot);
			Assert.IsNotNull(root, "Deserialized Rootobject is null");
		}

		[Test]
		public void VerifyMembers()
		{
			Root root = this.json.Deserialize<Root>(this.sampleRoot);
			Assert.IsNotNull(root, "Deserialized Rootobject is null");

			Assert.IsNotNull(root.Jobs, "Jobs member is null");
			Assert.AreEqual("NORMAL", root.Mode, "Mode is not NORMAL");
		}
	}
}
