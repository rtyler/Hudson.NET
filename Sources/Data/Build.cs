using System;
using System.Web.Script.Serialization;

namespace Hudson.Data
{
	[Serializable]
	public class Build
	{
		#region "Member Variables"
		protected int number = 0;
		protected string url = null;
		#endregion

		#region "Properties"
		public int Number
		{
			get { return this.number; }
			set { this.number = value; }
		}
		
		public string Url
		{
			get { return this.url; }
			set { this.url = value; }
		}
		#endregion
	}

}
