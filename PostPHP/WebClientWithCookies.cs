using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;					// Per WebClient e CookieContainer

namespace PostPHP
	{
	class WebClientWithCookies : WebClient
		{	// From http://www.codeproject.com/Articles/624624/Using-a-Cookie-Aware-WebClient-to-Persist-Authenti
		public CookieContainer CookieContainer { get; private set; }
		public WebClientWithCookies()
			{
			CookieContainer = new CookieContainer();
			}
		protected override WebRequest GetWebRequest(Uri address)
			{
			var request = (HttpWebRequest)base.GetWebRequest(address);	// Grabs the base request being made 
			request.CookieContainer = CookieContainer;					// Adds the existing cookie container to the Request
			return request;
			}
		}
	}
