using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

using NUnit.Framework;


namespace Hudson.Tests
{
	public class TestBase
	{
		#region "Member Variables"
		protected JavaScriptSerializer json = null;
		#endregion

		[SetUp]
		public void SetUp()
		{
			this.json = new JavaScriptSerializer();
		}
	}

	public class BadMockRequestException : Exception
	{
		public BadMockRequestException() : base() {} 
		public BadMockRequestException(string message) : base(message) {} 
	} 

	public class MockRequestProxy : Hudson.Internal.IRequestProxy
	{
		protected bool shouldUseSSL = false; 
		protected JavaScriptSerializer json = null;
		protected Dictionary<string, string> testData = null;

		#region "Public Constructors"
		public MockRequestProxy(Dictionary<string, string> testData)
		{
			this.testData = testData;
			this.json = new JavaScriptSerializer();
		}
		#endregion


		#region "Public Properties"
		public string ProtocolPrefix
		{
			get { return "mock"; }
		}

		public int Port
		{
			get { return 666; }
		}

		public bool useSSL
		{
			get { return this.shouldUseSSL; }
			set { this.shouldUseSSL = value; }
		}
		#endregion

		#region "Public Methods"
		public T Execute<T>(string endPoint)
		{
			if (this.testData.ContainsKey(endPoint) == false)
			{
				throw new BadMockRequestException(String.Format(
					"I don't have any data for {0}", endPoint));
			}
			Console.WriteLine(this.testData[endPoint]);
			return this.json.Deserialize<T>(this.testData[endPoint]);
		}

		public Dictionary<string, object> Execute(string endPoint)
		{
			if (this.testData.ContainsKey(endPoint) == false)
			{
				throw new BadMockRequestException(String.Format(
					"I don't have any data for {0}", endPoint));
			}
			return this.json.DeserializeObject(this.testData[endPoint]) 
				as Dictionary<string, object>;
		}
		#endregion
	}
}
