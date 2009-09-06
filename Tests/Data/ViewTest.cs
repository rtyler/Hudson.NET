using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

using NUnit.Framework;

using Hudson.Data;

namespace Hudson.Tests.ViewTests
{
	[TestFixture]
	public class ViewTest : Hudson.Tests.TestBase
	{
		protected readonly string sampleRootData = "{\"name\":\"All\",\"url\":\"http://localhost:8080/\"}";
		protected readonly string sampleViewDetailData = "{\"description\":\"This is a secondary view\",\"jobs\":[{\"name\":\"Downstream\",\"url\":\"http://localhost:8080/job/Downstream/\",\"color\":\"blue\"},{\"name\":\"Sleeper\",\"url\":\"http://localhost:8080/job/Sleeper/\",\"color\":\"blue\"}],\"name\":\"TestView\",\"url\":\"http://localhost:8080/view/TestView/\"}";

		[Test]
		public void BasicSimpleDeserialization()
		{
			View view = this.json.Deserialize<View>(this.sampleRootData);
			Assert.IsNotNull(view, "Deserialized View object is null");
		}

		[Test]
		public void BasicDetailedDeserialization()
		{
			View view = this.json.Deserialize<View>(this.sampleViewDetailData);
			Assert.IsNotNull(view, "Deserialized (detailed) View object is null");
		}

		[Test]
		public void VerifySimpleMembers()
		{
			View view = this.json.Deserialize<View>(this.sampleRootData);

			Assert.AreEqual("All", view.Name, "Simple View.Name should be \"All\"");
			Assert.AreEqual("http://localhost:8080/", view.Url, "Simple View.Url is wrong");

			Assert.IsNull(view.Description, "View.Description should be null for simple data");
		}

		[Test]
		public void VerifyDetailedMembers()
		{
			View view = this.json.Deserialize<View>(this.sampleViewDetailData);

			Assert.AreEqual("TestView", view.Name);
			Assert.AreEqual("This is a secondary view", view.Description);
			Assert.AreEqual(2, view.Jobs.Count);
			Assert.AreEqual("http://localhost:8080/view/TestView/", view.Url);
		}
	}
}
