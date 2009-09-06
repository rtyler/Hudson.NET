using System;
using System.Collections.Generic;

namespace Hudson
{
	public class Api
	{
		#region "Member Variables"
		protected Hudson.Internal.IRequestProxy requestProxy = null;
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

		public Api(Hudson.Internal.IRequestProxy rp)
		{
			if (rp == null)
			{
				throw new Exception("You cannot pass a null RequestProxy object to me");
			}
			this.requestProxy = rp;
		}
		#endregion

		#region "Internal Synchronous Methods"
		internal Hudson.Data.Root FetchRootData()
		{
			Hudson.Data.Root root = this.requestProxy.Execute<Hudson.Data.Root>("/api/json");

			if (root == null) 
			{
				/*
				 * TODO: Raise a pertinent exception
				 */
				Console.WriteLine("root is null");
			}

			return root;
		}
		#endregion

		#region "Synchronous Methods"
		public List<Hudson.Data.Project> FetchProjects()
		{
			Hudson.Data.Root root = this.FetchRootData();

			if (root == null)
			{
				return null;
			}

			return root.Jobs;
		}

		public List<Hudson.Data.Job> FetchJobs()
		{
			Hudson.Data.Root root = this.FetchRootData();

			if (root == null)
			{
				return null;
			}

			if (root.Jobs.Count == 0)
			{
				return new List<Hudson.Data.Job>();
			}

			List<Hudson.Data.Job> jobs = new List<Hudson.Data.Job>();
			foreach (Hudson.Data.Project project in root.Jobs) 
			{
				Hudson.Data.Job job = this.FetchJob(project.Name);
				if (job == null)
					continue;

				jobs.Add(job);
			}
			return jobs;	
		}

		public Hudson.Data.Job FetchJob(string jobName)
		{
			if (String.IsNullOrEmpty(jobName))
			{
				return null;
			}
			string endpoint = String.Format("/job/{0}/api/json", jobName);
			return this.requestProxy.Execute<Hudson.Data.Job>(endpoint);
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
