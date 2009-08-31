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
}
