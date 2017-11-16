using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InjectCountryCodeInURL
{
    public static class SiteGlobalization
    {
        static Dictionary<string, SiteCulture> BBaCulture = new Dictionary<string, SiteCulture>()
        {
            { "DE", new SiteCulture {CultureInfo="de-DE", UICultureInfo="de-DE", TwoDigitISORegionCode="DE",ThreeDigitISORegionCode="DEU",Visible=true}},
            { "ES", new SiteCulture {CultureInfo="es-ES", UICultureInfo="es-ES", TwoDigitISORegionCode="ES",ThreeDigitISORegionCode="ESP",Visible=true}},
            { "FR", new SiteCulture {CultureInfo="fr-FR", UICultureInfo="fr-FR", TwoDigitISORegionCode="FR",ThreeDigitISORegionCode="FRA",Visible=true}},
            { "MX", new SiteCulture {CultureInfo="es-MX", UICultureInfo="es-MX", TwoDigitISORegionCode="MX",ThreeDigitISORegionCode="MEX",Visible=true}},
            { "NL", new SiteCulture {CultureInfo="nl-NL", UICultureInfo="fr-FR", TwoDigitISORegionCode="NL",ThreeDigitISORegionCode="NLD",Visible=true}},
            { "PL", new SiteCulture {CultureInfo="pl-PL", UICultureInfo="pl-PL", TwoDigitISORegionCode="PL",ThreeDigitISORegionCode="POL",Visible=true}},
            { "GB", DefaultCulture},
        };

        public static Dictionary<string, SiteCulture> BBACultures
        {
            get
            {
                return BBaCulture;
            }
        }

        //NB United Kingdom is GB
        public static SiteCulture DefaultCulture
        {
            get
            {
                return new SiteCulture { CultureInfo = "en-GB", UICultureInfo = "en-GB", TwoDigitISORegionCode = "GB", ThreeDigitISORegionCode = "GBR", Visible = true };
            }
        }
    }

    public class SiteCulture
    {
        public string TwoDigitISORegionCode { get; set; }
        public string ThreeDigitISORegionCode { get; set; }
        public string CultureInfo { get; set; }
        public string UICultureInfo { get; set; }
        public bool Visible { get; set; }
    }
}