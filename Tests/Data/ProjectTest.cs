using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

using NUnit.Framework;

using Hudson.Data;

namespace Hudson.Tests.ProjectTests
{
	[TestFixture]
	public class ProjectTest : Hudson.Tests.TestBase
	{
		protected readonly string sampleDownstream = "{\"name\":\"Downstream\",\"url\":\"http://localhost:8080/job/Downstream/\",\"color\":\"grey\"}";	

		[Test]
		public void BasicDeserialization()
		{
			Project project = this.json.Deserialize<Project>(this.sampleDownstream);
			Assert.IsNotNull(project, "Deserialized Project object is null");
		}
	}
}
