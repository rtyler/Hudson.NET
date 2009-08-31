using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

using NUnit.Framework;

using Hudson.Data;

namespace Hudson.Tests.JobTests
{
	[TestFixture]
	public class SimpleFreeStyleDeserializationTests
	{
		protected readonly string sampleFreestyle = "{\"actions\":[],\"description\":\"\",\"displayName\":\"SimpleFreestyle\",\"name\":\"SimpleFreestyle\",\"url\":\"http://localhost:8080/job/SimpleFreestyle/\",\"buildable\":true,\"builds\":[{\"number\":3,\"url\":\"http://localhost:8080/job/SimpleFreestyle/3/\"},{\"number\":2,\"url\":\"http://localhost:8080/job/SimpleFreestyle/2/\"},{\"number\":1,\"url\":\"http://localhost:8080/job/SimpleFreestyle/1/\"}],\"color\":\"blue\",\"firstBuild\":{\"number\":1,\"url\":\"http://localhost:8080/job/SimpleFreestyle/1/\"},\"healthReport\":[{\"description\":\"Build stability: No recent builds failed.\",\"iconUrl\":\"health-80plus.gif\",\"score\":100}],\"inQueue\":false,\"keepDependencies\":false,\"lastBuild\":{\"number\":3,\"url\":\"http://localhost:8080/job/SimpleFreestyle/3/\"},\"lastCompletedBuild\":{\"number\":3,\"url\":\"http://localhost:8080/job/SimpleFreestyle/3/\"},\"lastFailedBuild\":null,\"lastStableBuild\":{\"number\":3,\"url\":\"http://localhost:8080/job/SimpleFreestyle/3/\"},\"lastSuccessfulBuild\":{\"number\":3,\"url\":\"http://localhost:8080/job/SimpleFreestyle/3/\"},\"nextBuildNumber\":4,\"property\":[],\"queueItem\":null,\"downstreamProjects\":[],\"upstreamProjects\":[]}";
		private JavaScriptSerializer json = null;

		[SetUp]
		public void SetUp()
		{
			this.json = new JavaScriptSerializer();
		}

		[Test]
		public void BasicDeserialization()
		{
			Job job = this.json.Deserialize<Job>(this.sampleFreestyle);
			Assert.IsNotNull(job, "Deserialized job is null");
		}

		[Test]
		public void CommonAttributes()
		{
			Job job = this.json.Deserialize<Job>(this.sampleFreestyle);

			Assert.AreEqual("SimpleFreestyle", job.DisplayName, "Invalid DisplayName");
			Assert.AreEqual("SimpleFreestyle", job.Name, "Invalid Name");

			Assert.IsNotNull(job.LastBuild, "LastBuild should not be null");
			Assert.AreEqual(3, job.LastBuild.Number, "LastBuild should be #3");
			Assert.AreEqual(4, job.NextBuildNumber, "NextBuildNumber should be 4");

			Assert.IsNotNull(job.Builds, "Builds should not be null");
			Assert.AreEqual(3, job.Builds.Count, "We should 3 elements in our Builds");

			Assert.IsNotNull(job.HealthReport, "HealthReport is null");
			Assert.AreEqual(1, job.HealthReport.Count, "HealthReport doesn't have 1 element");

			Assert.AreEqual("blue", job.Color, "Incorrect color");
		}
	}
}

