using System;
using System.Collections.Generic;

namespace Hudson
{
	public class Api
	{
		#region "Member Variables"
		protected Hudson.Internal.RequestProxy requestProxy = null;
		#endregion

		#region "Properties"
		#endregion

		#region "Constructors"
		public Api()
		{
			this.requestProxy = new Hudson.Internal.RequestProxy();
		}

		public Api(string host, int port)
		{
			this.requestProxy = new Hudson.Internal.RequestProxy(host, port);
		}
		#endregion

		#region "Synchronous Methods"
		public List<Hudson.Data.Project> FetchProjects()
		{
			Hudson.Data.Root root = this.requestProxy.Execute<Hudson.Data.Root>("/api/json");

			if (root == null) 
			{
				/*
				 * TODO: Raise a pertinent exception
				 */
				Console.WriteLine("root is null");
				return null;
			}

			return root.Jobs;
		}
		#endregion
	}

	public class SecureApi : Api
	{
		#region "Constructors"
		protected SecureApi() : base() { }

		public SecureApi(string host, int port) : base(host, port)
		{
			this.requestProxy.useSSL = true;
		}
		#endregion
	}
}
