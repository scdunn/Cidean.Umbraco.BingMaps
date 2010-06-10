using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using umbraco.interfaces;
using umbraco.editorControls;
using umbraco.cms.businesslogic;

namespace Cidean.Umbraco.bingMaps.DataTypes
{
    public class bingMapPlace : umbraco.cms.businesslogic.datatype.BaseDataType, umbraco.interfaces.IDataType
    {
        private umbraco.interfaces.IDataEditor _Editor;
        private umbraco.interfaces.IData _baseData;
        private bingMapPlacePrevalueEditor _prevalueeditor;

        public override umbraco.interfaces.IDataEditor DataEditor
        {
            get
            {
                if (_Editor == null)
                    _Editor = new bingMapPlaceEditor(Data, ((bingMapPlacePrevalueEditor)PrevalueEditor).Configuration);
                return _Editor;
            }
        }

        public override umbraco.interfaces.IData Data
        {
            get
            {
                if (_baseData == null)
                    _baseData = new umbraco.cms.businesslogic.datatype.DefaultData(this);
                return _baseData;
            }
        }
        public override Guid Id
        {
            get { return new Guid("A8525FA0-3873-11DF-B6E3-C20856D89593"); }
        }

        public override string DataTypeName
        {
            get { return "bing Map Place"; }
        }

        public override umbraco.interfaces.IDataPrevalue PrevalueEditor
        {
            get
            {
                if (_prevalueeditor == null)
                    _prevalueeditor = new bingMapPlacePrevalueEditor(this);
                return _prevalueeditor;
            }
        }
    }



    public class bingMapPlaceEditor : System.Web.UI.UpdatePanel, umbraco.interfaces.IDataEditor
    {

        #region  Standard

        private umbraco.interfaces.IData _data;

        public bingMapPlaceEditor(umbraco.interfaces.IData Data, string Configuration)
        {
            _data = Data;
        }

        public virtual bool TreatAsRichTextEditor
        {
            get { return false; }
        }

        public bool ShowLabel
        {
            get { return true; }
        }

        public Control Editor { get { return this; } }

        #endregion

        private TextBox uxAddress;
        private TextBox uxLongitude;
        private TextBox uxLatitude;
        


        public void Save()
        {

            string value = "";

            value = "<value>";
            value += "<data>";
            value += "<address>" + uxAddress.Text + "</address>";
            value += "<longitude>" + uxLongitude.Text + "</longitude>";
            value += "<latitude>" + uxLatitude.Text + "</latitude>";
            value += "</data></value>";

            this._data.Value = value;

        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);


            umbraco.library.AddJquery();
            umbraco.library.RegisterJavaScriptFile("virtual earth", "http://ecn.dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.2");
            umbraco.library.RegisterJavaScriptFile("bingmaps", Page.ClientScript.GetWebResourceUrl(typeof(bingMapBasic), "Cidean.Umbraco.bingMaps.Scripts.bingMapsBasic.js"));
            umbraco.library.RegisterJavaScriptFile("bingmapplace", Page.ClientScript.GetWebResourceUrl(typeof(bingMapBasic), "Cidean.Umbraco.bingMaps.Scripts.bingMapPlace.js"));


            uxAddress = new TextBox();
            uxAddress.TextMode = TextBoxMode.MultiLine;
            uxAddress.Rows = 3;
            uxAddress.CssClass = "bingMapAddress umbEditorTextField";

            uxLongitude = new TextBox();
            uxLongitude.CssClass = "bingMapLongitude";
            uxLongitude.Width = 150;
            
            uxLatitude = new TextBox();
            uxLatitude.CssClass = "bingMapLatitude";
            uxLatitude.Width = 150;

            if (!string.IsNullOrEmpty(_data.Value.ToString()))
            {
                try
                {

                    DataSet ds = new DataSet();
                    System.IO.StringReader sr = new System.IO.StringReader(_data.Value.ToString());
                    try
                    {
                        ds.ReadXml(sr);
                        DataTable dt = ds.Tables[0];
                        uxAddress.Text = dt.Rows[0]["address"].ToString();
                        uxLongitude.Text = dt.Rows[0]["longitude"].ToString();
                        uxLatitude.Text = dt.Rows[0]["latitude"].ToString();
                    }
                    catch (Exception ex)
                    {
                        string s = ex.Message;
                    }
                }
                catch (Exception ex)
                {

                }

            }




            AddControl("Address", "Street or partial address.", uxAddress);

            base.ContentTemplateContainer.Controls.Add(new LiteralControl(@"
            <p><input type='button' style='border:solid 1px #363636;' value='Click to set Longitude/Latitude from Address' onclick='bingMapSetLatLong();' /></p>
            "));

            AddLiteral("<table width='500px' cellpadding=0 cellspacing=0>");

            AddLiteral("<tr>");
            AddLiteral("<td><label>Latitude</label><br/>");
            AddControl(uxLatitude);
            AddLiteral("<br/><small>Latitude coordinate of Place</small>");
            AddLiteral("<br/><br/></td>");

            AddLiteral("<td><label>Longitude</label><br/>");
            AddControl(uxLongitude);
            AddLiteral("<br/><small>Longitude coordinate of Place</small>");
            AddLiteral("<br/><br/></td>");
            AddLiteral("<tr>");

            AddLiteral("</table>");

            AddLiteral("<div class='bingMessage' style='font-weight:bold;font-size:8pt;'></div>");

            base.ContentTemplateContainer.Controls.Add(new LiteralControl(@"
            <div style='position:relative'><div id='myMap' style='position:absolute;height:100px;width:100px;display:none;' ></div></div>
            "));

        }


        private void AddLiteral(string text)
        {
            base.ContentTemplateContainer.Controls.Add(new LiteralControl(text));
        }
        private void AddControl(Control control)
        {
            base.ContentTemplateContainer.Controls.Add(control);
        }

        private void AddControl(string label, string description, Control control)
        {
            base.ContentTemplateContainer.Controls.Add(new LiteralControl("<p>"));
            base.ContentTemplateContainer.Controls.Add(new LiteralControl("<label>" + label + "</label><br/>"));
            base.ContentTemplateContainer.Controls.Add(control);
            base.ContentTemplateContainer.Controls.Add(new LiteralControl("<br/>"));
            base.ContentTemplateContainer.Controls.Add(new LiteralControl("<small>" + description + "</small>"));
            base.ContentTemplateContainer.Controls.Add(new LiteralControl("</p>"));
        }

    }


    public class bingMapPlacePrevalueEditor : System.Web.UI.WebControls.PlaceHolder, umbraco.interfaces.IDataPrevalue
    {
        #region IDataPrevalue Members

        // referenced datatype
        private umbraco.cms.businesslogic.datatype.BaseDataType _datatype;


        public bingMapPlacePrevalueEditor(umbraco.cms.businesslogic.datatype.BaseDataType DataType)
        {

            _datatype = DataType;
            setupChildControls();

        }

        private void setupChildControls() { }



        public Control Editor
        {
            get
            {
                return this;
            }
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        public void Save() { }

        protected override void Render(HtmlTextWriter writer) { }

        public string Configuration
        {
            get { return ""; }
        }

        #endregion


    }


}
