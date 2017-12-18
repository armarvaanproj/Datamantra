using Datamantra.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Datamantra.Web.Extensions
{
    public class DatamantraAPIClient : HttpClient
    {
        public DatamantraAPIClient()
        {
            if (!string.IsNullOrWhiteSpace(Utility.GetAppSettings("DatamantraAPI")))
            {
                string apiURL = Utility.GetAppSettings("DatamantraAPI");
                string appURL = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                Uri apiURI = new Uri(apiURL);
                this.BaseAddress = apiURI;
            }
            else
            {
                this.BaseAddress = new Uri(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority));
            }
            this.DefaultRequestHeaders.Accept.Clear();

            this.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


    }
}