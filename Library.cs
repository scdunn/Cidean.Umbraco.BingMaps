using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Cidean.Umbraco.bingMaps
{
    public class Library
    {
        public static string ConfigFileName()
        {
            return "/umbraco/plugins/bingmaps/bingmaps.config";
        }

        public static string GetApiKey()
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(HttpContext.Current.Server.MapPath(Library.ConfigFileName()));

                return ds.Tables[0].Rows[0]["apikey"].ToString();
            }
            catch (Exception ex)
            {
                return "";
            }


        }

        public static string stripString(string value)
        {
            if (string.IsNullOrEmpty(value)) 
                return "";
            else
                return value.Replace('\n', ' ').Replace('\r', ' ').Replace("'", "&rsquo;");
        }

    }
}
