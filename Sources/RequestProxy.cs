using System;
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
		#endregion
	
		#region "Public Constructors"
		public RequestProxy(string host, int port)
		{
			/*
			 * If our host string is bollocks, or if the port is not a valid 
			 * int for a port, we should raise an InvalidRequestException
			 */
			if ( (String.IsNullOrEmpty(host)) || ( (port == 0) || (port > 65535) ) )
			{
				throw new InvalidRequestException(
					String.Format("Invalid arguments for RequestProxy()! (host: {0}, port: {1})", 
								host, port));
			}
			this.hostName = host;
			this.hostPort = port;
		}
		#endregion

		#region "Public Methods"
		public string Execute(string endpoint)
		{
			HttpWebRequest request = null;
			HttpWebResponse response = null;
			StreamReader reader = null;
			
			try
			{
				request = WebRequest.Create(String.Format("http://{0}:{1}/{2}", 
							this.hostName, this.hostPort, endpoint)) as HttpWebRequest;
				request.UserAgent = "Hudson.NET";
				// Use a small timeout
				request.Timeout = 20 * 1000;

				using (response = request.GetResponse() as HttpWebResponse)
				{
					if ( (!request.HaveResponse) && (response != null) )
					{
						return null;
					}

					reader = new StreamReader(response.GetResponseStream());

					return reader.ReadToEnd();
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
					Console.WriteLine("The server returned \"{0}\", status {1} ({1:d})",
							errorResponse.StatusDescription, errorResponse.StatusCode);
				}
			}
			return null;
		}
		#endregion
	}
}