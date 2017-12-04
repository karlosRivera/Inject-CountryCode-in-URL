using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Threading;
using System.Globalization;


namespace InjectCountryCodeInURL
{
    public static class Utility
    {
        public static string GetCountryCodeFromUrl(string url)
        {
            string pat = @"^\/[a-zA-Z]{2}\/";
            Regex r = new Regex(pat, RegexOptions.IgnoreCase);
            Match m = r.Match(url); //eg. /index.aspx OR /gb/index.aspx
            return m.Success ? m.Groups[0].Value.Replace("/", string.Empty) : string.Empty;
        }

        public static string GetCookieValue()
        {
            var mfdCookieValue = string.Empty;
            if (HttpContext.Current.Request.Cookies["TestSiteCookie"] != null)
            {
                mfdCookieValue = HttpContext.Current.Request.Cookies["TestSiteCookie"].Value;
            }
            return mfdCookieValue;
        }

        public static void SaveCountryCookie(string data)
        {
            HttpCookie mfdCookie = new HttpCookie("TestSiteCookie");
            mfdCookie.Value = data;
            mfdCookie.Expires = DateTime.Now.AddDays(300);
            HttpContext.Current.Response.Cookies.Add(mfdCookie);
        }

        public static SiteCulture GetCulture(string countryCode)
        {
            //either from the current UI culture or from the URL string
            SiteCulture culture = SiteGlobalization.DefaultCulture;
            switch (countryCode.Length)
            {
                case 2:
                    culture = SiteGlobalization.BBACultures.Keys.Contains(countryCode.ToUpper()) ? SiteGlobalization.BBACultures[countryCode.ToUpper()] : culture;
                    break;
                case 3:
                    culture = SiteGlobalization.BBACultures.Values.SingleOrDefault(x => x.ThreeDigitISORegionCode == countryCode);
                    break;
            }
            return culture;
        }

        public static string Get2DigitCountryCodeFromBrowser()
        {
            string[] languages = HttpContext.Current.Request.UserLanguages;
            if (languages == null || languages.Count() == 0 || !SiteGlobalization.BBACultures.Values.Any(x => x.CultureInfo == languages[0]))
            {
                return SiteGlobalization.DefaultCulture.TwoDigitISORegionCode;
            }

            return SiteGlobalization.BBACultures.Values.FirstOrDefault(x => x.CultureInfo == languages[0]).TwoDigitISORegionCode;
        }

        public static void SetCountry(SiteCulture culture)
        {
            //reset cookie
            //BBAReman.CountryCookie.ResetCookie(culture.ThreeDigitISORegionCode);

            //set locale!
            SetUserLocale(culture.CultureInfo, culture.UICultureInfo);
        }

        public static void SetUserLocale(string culture = null,
            string uiCulture = null,
            string currencySymbol = null,
            bool setUiCulture = true,
            string allowedLocales = null)
        {
            // Use browser detection in ASP.NET
            if (string.IsNullOrEmpty(culture) && HttpContext.Current != null)
            {
                HttpRequest Request = HttpContext.Current.Request;

                // if no user lang leave existing but make writable
                if (Request.UserLanguages == null)
                {
                    Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentCulture.Clone() as CultureInfo;
                    if (setUiCulture)
                        Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentUICulture.Clone() as CultureInfo;

                    return;
                }

                culture = Request.UserLanguages[0];
            }
            else
                culture = culture.ToLower();

            if (!string.IsNullOrEmpty(uiCulture))
                setUiCulture = true;

            if (!string.IsNullOrEmpty(culture) && !string.IsNullOrEmpty(allowedLocales))
            {
                allowedLocales = "," + allowedLocales.ToLower() + ",";
                if (!allowedLocales.Contains("," + culture + ","))
                {
                    int i = culture.IndexOf('-');
                    if (i > 0)
                    {
                        culture = culture.Substring(0, i);
                        if (!allowedLocales.Contains("," + culture + ","))
                        {
                            // Always create writable CultureInfo
                            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentCulture.Clone() as CultureInfo;
                            if (setUiCulture)
                                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentUICulture.Clone() as CultureInfo;

                            return;
                        }
                    }
                }
            }

            if (string.IsNullOrEmpty(culture))
                culture = CultureInfo.InstalledUICulture.IetfLanguageTag;

            if (string.IsNullOrEmpty(uiCulture))
                uiCulture = culture;

            try
            {
                CultureInfo Culture = new CultureInfo(culture);
                if (currencySymbol != null && string.IsNullOrEmpty(currencySymbol))
                {
                    Culture.NumberFormat.CurrencySymbol = currencySymbol;
                }

                Thread.CurrentThread.CurrentCulture = Culture;

                if (setUiCulture)
                {
                    var UICulture = new CultureInfo(uiCulture);
                    Thread.CurrentThread.CurrentUICulture = UICulture;
                }
            }
            catch { }
        }

        public static RegionInfo ResolveCountry()
        {
            CultureInfo culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            if (culture != null)
                return new RegionInfo(culture.LCID);

            return new RegionInfo(2057);
        }

    }
}