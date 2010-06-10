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
    public partial class bingMapOptions : System.Web.UI.UserControl, umbraco.editorControls.userControlGrapper.IUsercontrolDataEditor
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

                umbracoValue = "<value>";
                umbracoValue +="<data>";
                umbracoValue +="<height>" + uxHeight.Text + "</height>";
                umbracoValue += "<width>" + uxHeight.Text + "</width>";
                umbracoValue += "<viewstyle>" + uxViewStyle.SelectedValue + "</viewstyle>";
                umbracoValue += "<dashboardmode>" + uxDashboardMode.SelectedValue + "</dashboardmode>";
                umbracoValue += "<showminimap>" + uxShowMiniMap.Checked.ToString() + "</showminimap>";
                umbracoValue += "<hidescalebar>" + uxHideScalebar.Checked.ToString() + "</hidescalebar>";
                umbracoValue += "</data></value>";
                

                return umbracoValue;

            }
            set
            {
                

                if (String.IsNullOrEmpty(value.ToString()))
                    return;

                DataSet ds = new DataSet();
                System.IO.StringReader sr = new System.IO.StringReader( value.ToString()  );
                try
                {
                    ds.ReadXml(sr);
                    DataTable dt = ds.Tables[0];
                    uxHeight.Text = dt.Rows[0]["height"].ToString();
                    uxWidth.Text = dt.Rows[0]["width"].ToString();
                    uxViewStyle.SelectedValue = dt.Rows[0]["viewstyle"].ToString();
                    uxDashboardMode.SelectedValue = dt.Rows[0]["dashboardmode"].ToString();
                    uxShowMiniMap.Checked = bool.Parse(dt.Rows[0]["showminimap"].ToString());
                    uxHideScalebar.Checked = bool.Parse(dt.Rows[0]["hidescalebar"].ToString());
                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                }



                /*umbracoValue = value.ToString();
                if (!String.IsNullOrEmpty(umbracoValue))
                {
                    Array values = value.ToString().Split('|');
                    uxAddress.Text = values.GetValue(0).ToString();
                    uxLongitude.Text = values.GetValue(1).ToString();
                    uxLatitude.Text = values.GetValue(2).ToString();
                }
                */
            }
        }
    }
}