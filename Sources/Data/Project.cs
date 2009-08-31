using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Hudson.Data
{
	[Serializable]
	public class Project
	{
		#region "Member Variables"
		protected string name = null;
		protected string url = null;
		protected string color = null;
		#endregion

		#region "Properties"
		public string Name
		{
			get { return this.name; }
			set { this.name = value; }
		}

		public string Url
		{
			get { return this.url; }
			set { this.url = value; }
		}

		public string Color
		{
			get { return this.color; }
			set { this.color = value; }
		}
		#endregion
	}
}
