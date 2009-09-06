using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Hudson.Data
{
	[Serializable]
	public class View
	{
		#region "Member Variables"
		protected string description = null;
		protected List<Project> jobs = null;
		protected string name = null;
		protected string url = null;
		#endregion

		#region "Public Properties"
		public string Description
		{
			get { return this.description; }
			set { this.description = value; }
		}

		public List<Project> Jobs
		{
			get { return this.jobs; } 
			set { this.jobs = value; }
		}

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
		#endregion
	}
}
