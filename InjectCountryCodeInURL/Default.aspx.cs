using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InjectCountryCodeInURL
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string countrycode = Page.RouteData.Values["countrycode"] as string;

            var basePath = String.Format("{0}{1}", Request.ApplicationPath, string.IsNullOrEmpty(Request.ApplicationPath) || !Request.ApplicationPath.EndsWith("/") ? "/" : "");
        }
    }
}