using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InjectCountryCodeInURL
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            currentCultureFlagImage.ImageUrl = string.Format("/images/{0}.gif", Utility.ResolveCountry().TwoLetterISORegionName.ToLower());
            currentCultureFlagImage.AlternateText = System.Threading.Thread.CurrentThread.CurrentCulture.DisplayName;

        }
    }
}