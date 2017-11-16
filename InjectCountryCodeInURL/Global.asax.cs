using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace InjectCountryCodeInURL
{
    public class Global : HttpApplication
    {
        private const string _DEFAULTCOUNTRYCODE = "gb";
        private const string _DEFAULTCOUNTRYCODETHREECHAR = "GBR";


        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            string rawUrl = HttpContext.Current.Request.RawUrl;

            /* check the url has a country code in the title */
            //if (!HttpContext.Current.Request.CurrentExecutionFilePathExtension.Equals(".aspx"))
            //{
            //    if (HttpContext.Current.Request.CurrentExecutionFilePathExtension.Equals(string.Empty) && (HttpContext.Current.Request.FilePath.Length == 3))
            //    {
            //        //Could be countrycode with no ending slash
            //        rawUrl = rawUrl + "/";
            //    }
            //    else
            //        return;
            //}

            //check for 2 characters wrapped in forward slashes eg. /en/
            string countryCode = Utility.GetCountryCodeFromUrl(rawUrl);
            SiteCulture bbaCulture = SiteGlobalization.DefaultCulture;


            if (string.IsNullOrEmpty(countryCode))
            {
                //check cookie
                if (!string.IsNullOrEmpty(Utility.GetCookieValue()))
                {
                    string threeDigitIsoRegionCodeCookie = Utility.GetCookieValue();
                    bbaCulture = SiteGlobalization.BBACultures.Values.FirstOrDefault(x => x.ThreeDigitISORegionCode.Equals(threeDigitIsoRegionCodeCookie, StringComparison.InvariantCultureIgnoreCase));
                    if (bbaCulture != null && !string.IsNullOrEmpty(bbaCulture.TwoDigitISORegionCode))
                    {
                        countryCode = bbaCulture.TwoDigitISORegionCode;
                    }
                }

                //check the referer.
                if (string.IsNullOrEmpty(countryCode) && HttpContext.Current.Request.UrlReferrer != null && !string.IsNullOrEmpty(HttpContext.Current.Request.UrlReferrer.AbsolutePath))
                {
                    countryCode = Utility.GetCountryCodeFromUrl(HttpContext.Current.Request.UrlReferrer.AbsolutePath);
                }

                //finally browser
                countryCode = string.IsNullOrEmpty(countryCode) ? Utility.Get2DigitCountryCodeFromBrowser().ToLower() : countryCode;

                string redirectUrl = string.Format("/{0}{1}", countryCode, rawUrl);
                HttpContext.Current.Response.RedirectPermanent(redirectUrl.ToLower());
            }

            /* is the country code valid ? */
            bbaCulture = SiteGlobalization.BBACultures.Values.FirstOrDefault(x => x.TwoDigitISORegionCode.Equals(countryCode, StringComparison.InvariantCultureIgnoreCase));

            if (bbaCulture == null || string.IsNullOrEmpty(bbaCulture.ThreeDigitISORegionCode))
            {
                HttpContext.Current.Response.RedirectPermanent(string.Format("/{0}/index.aspx", SiteGlobalization.DefaultCulture.TwoDigitISORegionCode.ToLower()));
            }

            if (HttpContext.Current.Request.FilePath.EndsWith("/" + countryCode))
            {
                HttpContext.Current.Response.RedirectPermanent(string.Format("/{0}/index.aspx", countryCode.ToLower()));
            }

            /* set the default cutlure */
            Utility.SetCountry(bbaCulture);
        }

        private string getPathUrl(string path, string urlCountryChar)
        {
            string returnUrl = path;
            string hostName = System.Web.HttpContext.Current.Request.Url.Port != 80 ? System.Web.HttpContext.Current.Request.Url.Host + ":" + System.Web.HttpContext.Current.Request.Url.Port : System.Web.HttpContext.Current.Request.Url.Host;

            //remove duplicate slashes and application paths!
            string applicationPathUrl = string.Format("{0}{1}", System.Web.HttpContext.Current.Request.ApplicationPath.ToLower(), path).Replace("//", "/");
            returnUrl = string.Format("{0}://{1}{2}", System.Web.HttpContext.Current.Request.Url.Scheme, hostName, applicationPathUrl);
            return returnUrl;
        }
    }
}