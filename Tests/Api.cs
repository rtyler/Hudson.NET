using System;
using System.Collections.Generic;
using NUnit.Framework;

using Hudson;
using Hudson.Data;

namespace Hudson.Tests
{ 
	public class ApiBase : Hudson.Tests.TestBase
	{
		#region "Member Variables"
		protected Api api = null;
		protected MockRequestProxy requestProxy = null;
		#endregion

		[SetUp]
		public void SetUp()
		{
			// Using the default configuration (localhost:8080) for testing
			this.api = new Api();
		}
	}

	[TestFixture]
	public class ApiConstructorTests : Hudson.Tests.TestBase
	{
		[Test]
		public void BasicConstructor()
		{
			Api api = new Api();

			Assert.IsNotNull(api);
		}

		[Test]
		public void ParameterConstructor()
		{
			Api api = new Api("localhost", 8080);

			Assert.IsNotNull(api);
		}

		[Test]
		public void RequestProxyConstructor()
		{
			Api api = new Api(new Hudson.Internal.RequestProxy());

			Assert.IsNotNull(api);
		}
	}

	[TestFixture]
	public class FetchProjectsTest : ApiBase
	{
		[SetUp]
		public virtual void SetUp()
		{
			Dictionary<string, string> testData = new Dictionary<string, string>();
			testData.Add("/api/json", "{\"assignedLabels\":[{}],\"mode\":\"NORMAL\",\"nodeDescription\":\"the master Hudson node\",\"nodeName\":\"\",\"numExecutors\":2,\"description\":null,\"jobs\":[{\"name\":\"Downstream\",\"url\":\"http://localhost:8080/job/Downstream/\",\"color\":\"blue\"},{\"name\":\"Hudson.NET\",\"url\":\"http://localhost:8080/job/Hudson.NET/\",\"color\":\"blue\"},{\"name\":\"Sleeper\",\"url\":\"http://localhost:8080/job/Sleeper/\",\"color\":\"blue\"}],\"primaryView\":{\"name\":\"All\",\"url\":\"http://localhost:8080/\"},\"slaveAgentPort\":0,\"useCrumbs\":false,\"useSecurity\":false,\"views\":[{\"name\":\"All\",\"url\":\"http://localhost:8080/\"},{\"name\":\"TestView\",\"url\":\"http://localhost:8080/view/TestView/\"}]}");

			this.requestProxy = new Hudson.Tests.MockRequestProxy(testData);
			this.api = new Api(this.requestProxy);
		} 

		[Test]
		public void SynchronousFetch()
		{
			List<Project> projects = null;
			projects = this.api.FetchProjects();

			Assert.IsNotNull(projects, "FetchProjects() returned null");
		}
	}

	[TestFixture]
	public class FetchJobsTest : ApiBase
	{
		[SetUp]
		public virtual void SetUp()
		{
			Dictionary<string, string> testData = new Dictionary<string, string>();
			testData.Add("/api/json", "{\"assignedLabels\":[{}],\"mode\":\"NORMAL\",\"nodeDescription\":\"the master Hudson node\",\"nodeName\":\"\",\"numExecutors\":2,\"description\":null,\"jobs\":[{\"name\":\"Hudson.NET\",\"url\":\"http://localhost:8080/job/Hudson.NET/\",\"color\":\"blue\"}],\"primaryView\":{\"name\":\"All\",\"url\":\"http://localhost:8080/\"},\"slaveAgentPort\":0,\"useCrumbs\":false,\"useSecurity\":false,\"views\":[{\"name\":\"All\",\"url\":\"http://localhost:8080/\"},{\"name\":\"TestView\",\"url\":\"http://localhost:8080/view/TestView/\"}]}");
			testData.Add("/job/Hudson.NET/api/json", 
					"{\"actions\":[{},{}],\"description\":\"Hudson.NET self-building project\",\"displayName\":\"Hudson.NET\",\"name\":\"Hudson.NET\",\"url\":\"http://localhost:8080/job/Hudson.NET/\",\"buildable\":true,\"builds\":[{\"number\":1,\"url\":\"http://localhost:8080/job/Hudson.NET/1/\"}],\"color\":\"blue\",\"firstBuild\":{\"number\":1,\"url\":\"http://localhost:8080/job/Hudson.NET/1/\"},\"healthReport\":[{\"description\":\"Test Result: 0 tests failing out of a total of 23 tests.\",\"iconUrl\":\"health-80plus.gif\",\"score\":100},{\"description\":\"Build stability: No recent builds failed.\",\"iconUrl\":\"health-80plus.gif\",\"score\":100}],\"inQueue\":false,\"keepDependencies\":false,\"lastBuild\":{\"number\":159,\"url\":\"http://localhost:8080/job/Hudson.NET/159/\"},\"lastCompletedBuild\":{\"number\":159,\"url\":\"http://localhost:8080/job/Hudson.NET/159/\"},\"lastFailedBuild\":{\"number\":149,\"url\":\"http://localhost:8080/job/Hudson.NET/149/\"},\"lastStableBuild\":{\"number\":159,\"url\":\"http://localhost:8080/job/Hudson.NET/159/\"},\"lastSuccessfulBuild\":{\"number\":159,\"url\":\"http://localhost:8080/job/Hudson.NET/159/\"},\"nextBuildNumber\":160,\"property\":[],\"queueItem\":null,\"downstreamProjects\":[],\"upstreamProjects\":[]}");

			this.requestProxy = new Hudson.Tests.MockRequestProxy(testData);
			this.api = new Api(this.requestProxy);
		}

		[Test]
		public void SynchronousFetch()
		{
			List<Job> jobs = null;
			jobs = this.api.FetchJobs();

			Assert.IsNotNull(jobs, "FetchJobs() returned null");

			foreach (Job job in jobs) 
			{
				Assert.IsNotNull(job, "A job in the list was null!");
			}
		}
	}

	[TestFixture]
	public class FetchJobTest : ApiBase
	{
		[SetUp]
		public virtual void SetUp()
		{
			Dictionary<string, string> testData = new Dictionary<string, string>();
			testData.Add("/job/Hudson.NET/api/json", 
					"{\"actions\":[{},{}],\"description\":\"Hudson.NET self-building project\",\"displayName\":\"Hudson.NET\",\"name\":\"Hudson.NET\",\"url\":\"http://localhost:8080/job/Hudson.NET/\",\"buildable\":true,\"builds\":[{\"number\":1,\"url\":\"http://localhost:8080/job/Hudson.NET/1/\"}],\"color\":\"blue\",\"firstBuild\":{\"number\":1,\"url\":\"http://localhost:8080/job/Hudson.NET/1/\"},\"healthReport\":[{\"description\":\"Test Result: 0 tests failing out of a total of 23 tests.\",\"iconUrl\":\"health-80plus.gif\",\"score\":100},{\"description\":\"Build stability: No recent builds failed.\",\"iconUrl\":\"health-80plus.gif\",\"score\":100}],\"inQueue\":false,\"keepDependencies\":false,\"lastBuild\":{\"number\":159,\"url\":\"http://localhost:8080/job/Hudson.NET/159/\"},\"lastCompletedBuild\":{\"number\":159,\"url\":\"http://localhost:8080/job/Hudson.NET/159/\"},\"lastFailedBuild\":{\"number\":149,\"url\":\"http://localhost:8080/job/Hudson.NET/149/\"},\"lastStableBuild\":{\"number\":159,\"url\":\"http://localhost:8080/job/Hudson.NET/159/\"},\"lastSuccessfulBuild\":{\"number\":159,\"url\":\"http://localhost:8080/job/Hudson.NET/159/\"},\"nextBuildNumber\":160,\"property\":[],\"queueItem\":null,\"downstreamProjects\":[],\"upstreamProjects\":[]}");

			this.requestProxy = new Hudson.Tests.MockRequestProxy(testData);
			this.api = new Api(this.requestProxy);
		}

		[Test]
		public void SynchronousFetch()
		{
			Job job = this.api.FetchJob("Hudson.NET");

			Assert.IsNotNull(job, "FetchJob() returned null");
		}

		[Test]
		public void SynchronousFetchWithEmptyJobName()
		{
			Job job = this.api.FetchJob(String.Empty);

			Assert.IsNull(job, "FetchJob() should have returned null");
		}
	}

	[TestFixture]
	public class FetchViewTests : ApiBase
	{
		[SetUp]
		public virtual void SetUp()
		{
			Dictionary<string, string> testData = new Dictionary<string, string>();
			testData.Add("/api/json", "{\"assignedLabels\":[{}],\"mode\":\"NORMAL\",\"nodeDescription\":\"the master Hudson node\",\"nodeName\":\"\",\"numExecutors\":2,\"description\":null,\"jobs\":[{\"name\":\"Downstream\",\"url\":\"http://localhost:8080/job/Downstream/\",\"color\":\"blue\"},{\"name\":\"Hudson.NET\",\"url\":\"http://localhost:8080/job/Hudson.NET/\",\"color\":\"blue\"},{\"name\":\"Sleeper\",\"url\":\"http://localhost:8080/job/Sleeper/\",\"color\":\"blue\"}],\"primaryView\":{\"name\":\"All\",\"url\":\"http://localhost:8080/\"},\"slaveAgentPort\":0,\"useCrumbs\":false,\"useSecurity\":false,\"views\":[{\"name\":\"All\",\"url\":\"http://localhost:8080/\"},{\"name\":\"TestView\",\"url\":\"http://localhost:8080/view/TestView/\"}]}");
			testData.Add("/view/TestView/api/json", "{\"description\":\"This is a secondary view\",\"jobs\":[{\"name\":\"Downstream\",\"url\":\"http://localhost:8080/job/Downstream/\",\"color\":\"blue\"},{\"name\":\"Sleeper\",\"url\":\"http://localhost:8080/job/Sleeper/\",\"color\":\"blue\"}],\"name\":\"TestView\",\"url\":\"http://localhost:8080/view/TestView/\"}");	

			this.requestProxy = new Hudson.Tests.MockRequestProxy(testData);
			this.api = new Api(this.requestProxy);
		}

		[Test]
		public void SynchronousFetchView()
		{
			View view = this.api.FetchView("TestView");

			Assert.IsNotNull(view);
			Assert.IsNotNull(view.Description);
		}

		[Test]
		public void SynchronousFetchViews()
		{
			List<View> views = this.api.FetchViews();

			Assert.IsNotNull(views);
			Assert.AreEqual(2, views.Count);
		}
	}
}
