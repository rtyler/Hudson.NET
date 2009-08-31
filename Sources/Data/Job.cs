using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Hudson.Data
{
	/*
	 * Hudson.Data.Job - Serializable version of the Hudson Job JSON
	 *
	 * Sample: 

		{
			"actions": [
				
			],
			"description": "",
			"displayName": "SimpleFreestyle",
			"name": "SimpleFreestyle",
			"url": "http://localhost:8080/job/SimpleFreestyle/",
			"buildable": true,
			"builds": [
				
			],
			"color": "grey",
			"firstBuild": null,
			"healthReport": [
				
			],
			"inQueue": false,
			"keepDependencies": false,
			"lastBuild": null,
			"lastCompletedBuild": null,
			"lastFailedBuild": null,
			"lastStableBuild": null,
			"lastSuccessfulBuild": null,
			"nextBuildNumber": 1,
			"property": [
				
			],
			"queueItem": null,
			"downstreamProjects": [
				
			],
			"upstreamProjects": [
				
			]
		}
	 */
	[Serializable]
	public class Job
	{
		#region "Member Variables"
		protected string description = null;
		protected string displayName = null;
		protected string name = null;
		protected string url = null;
		protected bool buildable = false;
		protected List<Build> builds = null;
		protected string color = null;
		protected Build firstBuild = null;
		protected List<HealthReport> healthReport = null;

		protected bool inQueue = false;
		protected bool keepDependencies = false;

		protected Build lastBuild = null;
		protected Build lastCompletedBuild = null;
		protected Build lastFailedBuild = null; 
		protected Build lastStableBuild = null;
		protected Build lastSuccessfulBuild = null;
		protected int nextBuildNumber = 0;

		#endregion	

		#region "Properties"
		public string Description
		{
			get { return this.description; }
			set { this.description = value; }
		}

		public string DisplayName
		{
			get { return this.displayName; }
			set { this.displayName = value; }
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

		public bool Buildable 
		{
			get { return this.buildable; }
			set { this.buildable = value; }
		}

		public List<Build> Builds
		{
			get { return this.builds; }
			set { this.builds = value; }
		}

		public string Color
		{
			get { return this.color; }
			set { this.color = value; }
		}

		public Build FirstBuild 
		{
			get { return this.firstBuild; }
			set { this.firstBuild = value; }
		}

		public List<HealthReport> HealthReport
		{
			get { return this.healthReport; } 
			set { this.healthReport = value; }
		}

		public bool InQueue 
		{
			get { return this.inQueue; }
			set { this.inQueue = value; }
		}

		public bool KeepDependencies
		{
			get { return this.keepDependencies; }
			set { this.keepDependencies = value; }
		}

		public Build LastBuild
		{
			get { return this.lastBuild; }
			set { this.lastBuild = value; }
		}

		public Build LastCompletedBuild
		{
			get { return this.lastCompletedBuild; }
			set { this.lastCompletedBuild = value; }
		}

		public Build LastFailedBuild
		{
			get { return this.lastFailedBuild; }
			set { this.lastFailedBuild = value; }
		}

		public Build LastStableBuild
		{
			get { return this.lastStableBuild; }
			set { this.lastStableBuild = value; }
		}

		public Build LastSuccessfulBuild
		{
			get { return this.lastSuccessfulBuild; }
			set { this.lastSuccessfulBuild = value; }
		}

		public int NextBuildNumber
		{
			get { return this.nextBuildNumber; }
			set { this.nextBuildNumber = value; }
		}
		#endregion

		#region "Public Methods"
		#endregion
	}
}
