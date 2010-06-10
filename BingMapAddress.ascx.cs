using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Cidean.Umbraco.bingMaps
{
    public partial class bingMapAddress : System.Web.UI.UserControl, umbraco.editorControls.userControlGrapper.IUsercontrolDataEditor
    {
        public string umbracoValue;

        

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Page.IsPostBack)
            {

            }
        }

        public object value
        {
            get
            {
                umbracoValue = uxAddress.Text + "|" + uxLongitude.Text + "|" + uxLatitude.Text;
                return umbracoValue;

            }
            set
            {
                umbracoValue = value.ToString();
                if (!String.IsNullOrEmpty(umbracoValue))
                {
                    Array values = value.ToString().Split('|');
                    uxAddress.Text = values.GetValue(0).ToString();
                    uxLongitude.Text = values.GetValue(1).ToString();
                    uxLatitude.Text = values.GetValue(2).ToString();
                }
            }
        }
    }
}