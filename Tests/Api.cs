using System;
using System.Collections.Generic;
using NUnit.Framework;

using Hudson;
using Hudson.Data;

namespace Hudson.Tests
{ 
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
	public class FetchProjectsTest : Hudson.Tests.TestBase
	{
		protected Api api = null;

		[SetUp]
		public void SetUp() 
		{
			// Using the default configuration (localhost:8080) for testing
			this.api = new Api();
		}

		[Test]
		public void SynchronousFetch()
		{
			List<Project> projects = null;
			projects = this.api.FetchProjects();

			Assert.IsNotNull(projects, "FetchProjects() returned null");
		}
	}
}
