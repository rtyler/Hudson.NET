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
		}

		[Test]
		public void ParameterConstructor()
		{
			Api api = new Api("localhost", 8080);
		}
	}

	[TestFixture]
	public class FetchProjectsTest : ApiBase
	{
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
}
