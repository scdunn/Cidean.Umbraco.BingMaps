#region Namespace References
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using umbraco.BusinessLogic;
using umbraco.cms.businesslogic.web;
using System.Text;
using System.Xml.XPath;
#endregion

namespace Cidean.Umbraco.bingMaps
{
    public partial class bingMapAdvanced : System.Web.UI.UserControl
    {
        public int bingMapNode { get; set; }

        public class bingMapPlaceData
        {
            //public properties
            public string Address = "";
            public string Longitude = "";
            public string Latitude = "";

            //parse bingMapPlace data and return bingMapPlace object
            public bingMapPlaceData(string value)
            {
                if (!String.IsNullOrEmpty(value))
                {
                    DataSet placeDataSet = new DataSet();
                    System.IO.StringReader sr = new System.IO.StringReader(value);
                    try
                    {
                        placeDataSet.ReadXml(sr);
                        DataTable placeDataTable = placeDataSet.Tables[0];
                        Address = Library.stripString(placeDataTable.Rows[0]["address"].ToString());
                        Longitude = placeDataTable.Rows[0]["longitude"].ToString();
                        Latitude = placeDataTable.Rows[0]["latitude"].ToString();
                    }
                    catch (Exception ex)
                    {
                        string s = ex.Message;
                    }
                }
            }

        }

        //generate rss feed from child nodes of type bingMapLocation
        private void GetGeoRss(Document mapNode)
        {
            string output = "";
            output += @"<?xml version='1.0' encoding='UTF-8'?>
            <rss version='2.0' xmlns:geo='http://www.w3.org/2003/01/geo/wgs84_pos#' xmlns:georss='http://www.georss.org/georss' xmlns:gml='http://www.opengis.net/gml' xmlns:mappoint='http://virtualearth.msn.com/apis/annotate#'>
                    <channel>";



            if (mapNode.HasChildren)
            {
                foreach (Document locNode in mapNode.Children)
                {
                    if ((locNode.ContentType.Alias == "bingMapPlace") && (locNode.Published))
                    {
                        string val = locNode.getProperty("bingMapPlace").Value.ToString();
                        bingMapPlaceData locData = new bingMapPlaceData(val);

                            output += @"<item>";
                            output += "<title>" + locNode.getProperty("bingMapPlaceTitle").Value.ToString() + "</title>";
                            output += "<description><![CDATA[" + locNode.getProperty("bingMapPlaceDescription").Value.ToString() + "]]></description>";
                            output += "<link>" + locNode.getProperty("bingMapPlaceLink").Value.ToString() + "</link>";
                            output += "<geo:long>" + locData.Longitude + "</geo:long>";
                            output += "<geo:lat>" + locData.Latitude + "</geo:lat>";

                            string iconUrl = "";
                            try
                            {
                                iconUrl = "http://" + Request.Url.Host + (new umbraco.cms.businesslogic.media.Media(Int32.Parse(locNode.getProperty("bingMapPlaceIcon").Value.ToString()))).getProperty("umbracoFile").Value.ToString();
                            }
                            catch (Exception ex)
                            {

                            }
                            if (iconUrl != "")
                                output += "<icon>" + iconUrl + "</icon>";


                            output += "</item>";
                        
                    }
                }
            }

            output += "</channel> </rss> ";

            //format response and send georss feed to browser
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + DateTime.Now.ToString());
            Response.AddHeader("Content-Length", output.Length.ToString());
            Response.CacheControl = "no-cache";
            Response.AddHeader("Pragma", "no-cache");
            Response.Expires = 0;
            Response.ContentType = "application/xml";
            Response.Write(output);
            Response.End();
            return;
        }

        //display preview html in RichTextEditor
        private void ShowPreview()
        {
            string output = "";
            output += "<strong>bing Maps Advanced (www.cidean.com)</strong>";
            output += "<p>This macro requires javascript and cannot be previewed.  The macro will render properly on the page.</p>";
            this.Controls.Add(new LiteralControl(output));
        }

 

        protected override void OnLoad(EventArgs e)
        {

            //display preview information html within RichTextEditor
            if (Request.Path.Contains("/umbraco/macroResultWrapper.aspx"))
            {
                ShowPreview();
                return;
            }
            
            Document mapNode = new Document(bingMapNode);

            //return georss feed and exit
            if (Request.QueryString["xml"] == "true")
            {
                GetGeoRss(mapNode);
                return;
            }




            bingMapPlaceData mapLocation = new bingMapPlaceData(mapNode.getProperty("bingMapPlace").Value.ToString());

            
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            System.IO.StringReader sr = new System.IO.StringReader(mapNode.getProperty("bingMapOptions").Value.ToString());
            try
            {
                ds.ReadXml(sr);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            
            string placeData = "";
            bool hasLoc = false;

            //Document Properties from Map Node
            string height = dt.Rows[0]["height"].ToString();
            string width = dt.Rows[0]["width"].ToString();


            string showMiniMap = dt.Rows[0]["showminimap"].ToString().ToLower();
            string hideScalebar = dt.Rows[0]["hidescalebar"].ToString().ToLower();
            
            string dashboardMode = "";
            dashboardMode = dt.Rows[0]["dashboardmode"].ToString();
            string viewStyle = "";
            viewStyle = dt.Rows[0]["viewstyle"].ToString();
            


            if (mapNode.HasChildren)
            {
                foreach (Document locNode in mapNode.Children)
                {

                    if ((locNode.ContentType.Alias == "bingMapPlace") && (locNode.Published))
                        hasLoc = true;

                    if ((locNode.ContentType.Alias == "bingMapSearch") && (locNode.Published))
                    {
                        if (!String.IsNullOrEmpty(locNode.getProperty("bingMapSearchKeyword").Value.ToString()))
                        {
                            string iconUrl = "";
                            try
                            {
                                iconUrl = (new umbraco.cms.businesslogic.media.Media(Int32.Parse(locNode.getProperty("bingMapSearchIcon").Value.ToString()))).getProperty("umbracoFile").Value.ToString();
                            }
                            catch (Exception ex)
                            {
                               //error getting icon url or icon url not set 
                            }

                            placeData += ",new Array('" + locNode.getProperty("bingMapSearchKeyword").Value.ToString() + "','" + iconUrl + "')";

                        }
                    }
                }
            }

            


            if (!string.IsNullOrEmpty(placeData))
                placeData = "new Array(" + placeData.Substring(1) + ")";
            else
                placeData = "null";

            placeData = "$(document).ready(function(){bingGetAdvancedMap('" + Library.GetApiKey() + "','" + this.ClientID + "_map','" + mapLocation.Address + "'," + placeData + "," + hasLoc.ToString().ToLower() + ",'" + dashboardMode.ToLower() + "'," + showMiniMap + "," + hideScalebar + ",'" + viewStyle.ToLower() + "');});";

            //Add jquery and other script dependencies
            umbraco.library.AddJquery();
            umbraco.library.RegisterJavaScriptFile("virtual earth", "http://ecn.dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.2");
            umbraco.library.RegisterJavaScriptFile("bingMapAdvanced", Page.ClientScript.GetWebResourceUrl(typeof(bingMapBasic), "Cidean.Umbraco.bingMaps.Scripts.bingMapAdvanced.js"));
            Page.ClientScript.RegisterStartupScript(Page.GetType(), this.ClientID + "_load", placeData, true);
            this.Controls.Add(new LiteralControl("<div id='" + this.ClientID + "_map' style='position:relative; width:" + width + "; height:" + height + ";'></div>"));
            

        }



    }
}