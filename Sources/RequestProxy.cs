using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

using Hudson;

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
		public readonly int defaultPort = 8080;
		protected int hostPort = 0;
		public bool useSSL = false;

		protected JavaScriptSerializer json = null;
		#endregion
	
		#region "Public Constructors"
		public RequestProxy()
		{
			this.json = new JavaScriptSerializer();
			this.hostName = "localhost";
			this.hostPort = this.defaultPort;
		}

		public RequestProxy(string host) : this()
		{
			if (String.IsNullOrEmpty(host)) 
			{
				throw new InvalidRequestException("Invalid host argument to constructor");
			}
			this.hostName = host;
		}

		public RequestProxy(string host, int port) : this(host)
		{
			if ( (port <= 0) || (port > 65535) )
			{
				throw new InvalidRequestException("Invalid port number to constructor");
			}
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
		public int Port
		{
			get
			{
				return this.hostPort;
			}
		}
		#endregion 

		internal string FetchJson(string endpoint)
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
					if (errorResponse == null)
					{
						/*
						 * If we don't have an error response, that means we likely
						 * have a SocketException from the underlying layer, and we 
						 * should likely propogate that up
						 */
						throw;
					}
					Console.WriteLine("The server returned \"{0}\", status {1}",
							errorResponse.StatusDescription, errorResponse.StatusCode);
				}
			}
			return null;
		}

		#region "Public Methods"
		public T Execute<T>(string endpoint)
		{
			string result = this.FetchJson(endpoint);

			if (String.IsNullOrEmpty(result))
			{
				return default(T);
			}	

			return this.json.Deserialize<T>(result);
		}

		public Dictionary<string, object> Execute(string endpoint)
		{
			string result = this.FetchJson(endpoint);
			
			if (String.IsNullOrEmpty(result))
			{
				return null;
			}

			return this.json.DeserializeObject(result) as Dictionary<string, object>;
		}
		#endregion
	}
}
