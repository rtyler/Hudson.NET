using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace Hudson.Internal
{
	public class InvalidRequestException : Exception
	{
		public InvalidRequestException() : base() {}
		public InvalidRequestException(string message) : base(message) {}
	}

	public class RequestProxy
	{
		#region "Member Variables"
		protected string hostName = null;
		protected int hostPort = 8080;
		protected bool useSSL = false;

		protected JavaScriptSerializer json = null;
		#endregion
	
		#region "Public Constructors"
		public RequestProxy()
		{
			this.json = new JavaScriptSerializer();
		}

		public RequestProxy(string host, int port) : this()
		{
			/*
			 * If our host string is bollocks, or if the port is not a valid 
			 * int for a port, we should raise an InvalidRequestException
			 */
			if ( (String.IsNullOrEmpty(host)) || ( (port <= 0) || (port > 65535) ) )
			{
				throw new InvalidRequestException(
					String.Format("Invalid arguments for RequestProxy()! (host: {0}, port: {1})", 
								host, port));
			}
			this.hostName = host;
			this.hostPort = port;
		}
		#endregion

		#region "Properties"
		public string ProtocolPrefix
		{
			get
			{
				if (this.useSSL)
					return "https";
				return "http";
			}
		}
		#endregion 

		#region "Public Methods"
		public Dictionary<string, object> Execute(string endpoint)
		{
			HttpWebRequest request = null;
			HttpWebResponse response = null;
			StreamReader reader = null;

			try
			{
				request = WebRequest.Create(String.Format("{0}://{1}:{2}/{3}", 
							this.ProtocolPrefix, this.hostName, 
							this.hostPort, endpoint)) as HttpWebRequest;
				request.UserAgent = "Hudson.NET";
				// Use a small timeout
				request.Timeout = 20 * 1000;

				if (request == null)
				{
					return null;
				}

				using (response = request.GetResponse() as HttpWebResponse)
				{
					if ( (response == null) || (!request.HaveResponse) )
					{
						return null;
					}

					reader = new StreamReader(response.GetResponseStream());
					return this.json.DeserializeObject(reader.ReadToEnd()) as 
							Dictionary<string, object>;
				}
			}
			catch (WebException exc)
			{
				if (exc == null)
				{
					return null;
				}

				using (HttpWebResponse errorResponse = exc.Response as HttpWebResponse)
				{
					if (errorResponse == null)
					{
						Console.WriteLine("Failed to get any response?");
						return null;
					}
					Console.WriteLine("The server returned \"{0}\", status {1}",
							errorResponse.StatusDescription, errorResponse.StatusCode);
				}
			}
			return null;
		}
		#endregion
	}
}
