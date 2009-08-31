using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Hudson.Data
{
	[Serializable]
	public class QueueItem
	{
		#region "Member Variables"
		protected bool blocked = false;
		protected bool buildable = false;
		protected bool stuck = false;
		protected Dictionary<string, string> task = null;
		protected string why = null;
		protected Int64 buildableStartMilliseconds = 0;
		#endregion

		#region "Properties"
		public bool Blocked
		{
			get { return this.blocked; }
			set { this.blocked = value; }
		}

		public bool Buildable
		{
			get { return this.buildable; }
			set { this.buildable = value; }
		}

		public bool Stuck
		{
			get { return this.stuck; }
			set { this.stuck = value; }
		}

		public Dictionary<string, string> Task
		{
			get { return this.task; }
			set { this.task = value; }
		}

		public string Why
		{
			get { return this.why; }
			set { this.why = value; }
		}

		public Int64 BuildableStartMilliseconds
		{
			get { return this.buildableStartMilliseconds; }
			set { this.buildableStartMilliseconds = value; }
		}
		#endregion
	}

	/*
	 * Hudson.Data.Queue - Serializable version of the Hudson Queue JSON
	 *
	 * Sample:

		{
			"items": [
				{
					"actions": [
						{
							"causes": [
								{
									"shortDescription": "Started by user anonymous"
								}
							]
						}
					],
					"blocked": false,
					"buildable": true,
					"stuck": false,
					"task": {
						"name": "Sleeper",
						"url": "http://localhost:8080/job/Sleeper/",
						"color": "grey_anime"
					},
					"why": "Waiting for next available executor",
					"buildableStartMilliseconds": 1251693111920
				}
			]
		}
	 */
	[Serializable]
	public class Queue
	{
		#region "Member Variables"
		protected List<QueueItem> items = null;
		#endregion

		#region "Properties"
		public List<QueueItem> Items
		{
			get { return this.items; }
			set { this.items = value; }
		}
		#endregion
	}
}
