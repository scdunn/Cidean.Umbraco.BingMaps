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
    public partial class Install : System.Web.UI.UserControl
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            uxMessage.Visible = false;
            btnSave.Click += new EventHandler(btnSave_Click);

            if (!IsPostBack)
            {
                uxAPIKey.Text = Library.GetApiKey();
            }

        }

        void btnSave_Click(object sender, EventArgs e)
        {
            string filesource = @"<?xml version='1.0' standalone='yes'?>
            <config>
            <data>
            <apikey>APIKEYVALUE</apikey>
            </data>
            </config>";

            filesource = filesource.Replace("APIKEYVALUE", uxAPIKey.Text);

            System.IO.File.WriteAllText(MapPath(Library.ConfigFileName()), filesource);

            uxMessage.Visible = true;

        }
    }
}