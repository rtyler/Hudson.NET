using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Hudson.Data
{
	[Serializable]
	public class Root
	{
		#region "Member Variables"
		protected string mode = null;
		protected List<Project> jobs = null;
		protected string nodeDescription = null;
		protected int numExecutors = 0;
		protected string description = null;
		protected List<View> views = null;
		#endregion

		#region "Public Properties"
		public string Mode
		{
			get { return this.mode; }
			set { this.mode = value; }
		}

		public List<Project> Jobs
		{
			get { return this.jobs; } 
			set { this.jobs = value; }
		}

		public string NodeDescription
		{
			get { return this.nodeDescription; }
			set { this.nodeDescription = value; }
		}

		public int NumExecutors
		{
			get { return this.numExecutors; }
			set { this.numExecutors = value; }
		}
		
		public string Description
		{
			get { return this.description; }
			set { this.description = value; }
		}

		public List<View> Views
		{
			get { return this.views; }
			set { this.views = value; }
		}
		#endregion
	}
}
