using System;

namespace Hudson.Data
{
	public class HealthReport
	{
		#region "Member Variables"
		protected string description = null;
		protected string iconUrl = null;
		protected int score = 0;
		#endregion

		#region "Properties"
		public string Description
		{
			get { return this.description; }
			set { this.description = value; }
		}

		public string IconUrl
		{
			get { return this.iconUrl; }
			set { this.iconUrl = value; }
		}

		public int Score
		{
			get { return this.score; }
			set { this.score = value; }
		}
		#endregion
	}
}
