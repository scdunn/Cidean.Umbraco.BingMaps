#region Namespaces
    using System;
    using System.Data;
    using System.Configuration;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
#endregion

namespace Cidean.Umbraco.bingMaps
{
    public partial class bingMapBasic : System.Web.UI.UserControl
    {
        //Public Properties
        public string Where { get; set; }
        public string WhereTitle { get; set; }
        public string WhereDescription { get; set; }
        public string What { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        public bool HideDashboard { get; set; }
        public string Icon { get; set; }

        /// <summary>
        /// Show preview html when displayed in RichTextEditor
        /// </summary>
        private void ShowPreview()
        {
            string output = "";
            output += "<strong>bing Maps Basic (www.cidean.com)</strong>";
            output += "<p>This macro requires javascript and cannot be previewed.  The macro will render properly on the page.</p>";
            if(Where!=null)
                output += "<p><u>Location(Address)</u><br/>" + Where + "</p>";
            if(What!=null)
                output += "<p><u>Search Term</u><br/>" + What + "</p>";
            this.Controls.Add(new LiteralControl(output));
        }


        protected override void OnLoad(EventArgs e)
        {
            //Set default height and width of map if not specified
            if (Height == "") Height = "500px";
            if (Width == "") Width = "500px";

            //Show preview message when displaying in RichTextEditor
            if (Request.Path.Contains("/umbraco/macroResultWrapper.aspx"))
            {
                ShowPreview();
                return;
            }

            //Add Base javascript files to header
            umbraco.library.AddJquery();
            umbraco.library.RegisterJavaScriptFile("virtual earth", "http://ecn.dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.2");
            umbraco.library.RegisterJavaScriptFile("bingMapBasic", Page.ClientScript.GetWebResourceUrl(typeof(bingMapBasic), "Cidean.Umbraco.bingMaps.Scripts.bingMapBasic.js"));


            string iconUrl = "";
            try
            {
                iconUrl = (new umbraco.cms.businesslogic.media.Media(Int32.Parse(Icon))).getProperty("umbracoFile").Value.ToString();
                if (!string.IsNullOrEmpty(iconUrl))
                    iconUrl = "http://" + Request.Url.Host + iconUrl;
            }
            catch (Exception ex)
            {
                //error getting icon url or icon url not set 
            }

            //Add map initialization javascript function to header using parameters
            //of specified by user.
            string script = "$(document).ready(function(){bingGetBasicMap('" + Library.GetApiKey() + "','" + this.ClientID + "_bingMap"
                + "','" + Library.stripString(Where)
                + "','" + Library.stripString(WhereTitle)
                + "','" + Library.stripString(WhereDescription)
                + "','" + Library.stripString(What)
                + "','" + Library.stripString(iconUrl)
                + "'," + HideDashboard.ToString().ToLower()
                + ");});";
            Page.ClientScript.RegisterStartupScript(Page.GetType(), this.ClientID + "_bingMap", script, true);


            //Add map html to the page
            this.Controls.Add(new LiteralControl("<div id='" + this.ClientID + "_bingMap" + "' style='position:relative; width:" + Width + "; height:" + Height + ";'></div>"));
        }
    }
}