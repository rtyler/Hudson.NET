using System;
using System.Collections.Generic;

using Hudson;
using Hudson.Data;

namespace Hudson.Samples
{
	public class InteractiveCLI
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("");
			Console.WriteLine("=> Welcome to the Hudson.NET interactive CLI!");
			Api api = new Api();

			Console.WriteLine("Pinging your Hudson instance...");
			List<Project> projects = api.FetchProjects();

			if ( (projects == null) || (projects.Count == 0) )
			{
				Console.WriteLine("Looks like Hudson is empty :(");
				return;
			}

			foreach (Project proj in projects) 
			{
				Console.WriteLine("  {0}\t{1}", proj.Name, proj.Color);
			}
		}
	}
}
