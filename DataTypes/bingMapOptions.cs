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
    public class bingMapOptions : umbraco.cms.businesslogic.datatype.BaseDataType, umbraco.interfaces.IDataType
    {
        private umbraco.interfaces.IDataEditor _Editor;
        private umbraco.interfaces.IData _baseData;
        private bingMapOptionsPrevalueEditor _prevalueeditor;

        public override umbraco.interfaces.IDataEditor DataEditor
        {
            get
            {
                if (_Editor == null)
                    _Editor = new bingMapOptionsEditor(Data, ((bingMapOptionsPrevalueEditor)PrevalueEditor).Configuration);
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
            get { return new Guid("506AFF40-385F-11DF-A1EF-5F6055D89593"); }
        }

        public override string DataTypeName
        {
            get { return "bing Map Options"; }
        }

        public override umbraco.interfaces.IDataPrevalue PrevalueEditor
        {
            get
            {
                if (_prevalueeditor == null)
                    _prevalueeditor = new bingMapOptionsPrevalueEditor(this);
                return _prevalueeditor;
            }
        }
    }



    public class bingMapOptionsEditor : System.Web.UI.UpdatePanel, umbraco.interfaces.IDataEditor
    {

        #region  Standard

        private umbraco.interfaces.IData _data;

        public bingMapOptionsEditor(umbraco.interfaces.IData Data, string Configuration)
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

        private TextBox uxWidth;
        private DropDownList uxWidthUnits;
        private TextBox uxHeight;
        private DropDownList uxHeightUnits;
        private DropDownList uxViewStyle;
        private DropDownList uxDashboardMode;
        private CheckBox uxShowMiniMap;
        private CheckBox uxHideScalebar;


        public void Save()
        {

            string value = "";

            value = "<value>";
            value += "<data>";
            value += "<height>" + uxHeight.Text + "</height>";
            value += "<width>" + uxHeight.Text + "</width>";
            value += "<viewstyle>" + uxViewStyle.SelectedValue + "</viewstyle>";
            value += "<dashboardmode>" + uxDashboardMode.SelectedValue + "</dashboardmode>";
            value += "<showminimap>" + uxShowMiniMap.Checked.ToString() + "</showminimap>";
            value += "<hidescalebar>" + uxHideScalebar.Checked.ToString() + "</hidescalebar>";
            value += "</data></value>";

            this._data.Value = value;

        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            string width = "";
            string height = "";
            string viewstyle = "";
            string dashboardmode = "";
            string showminimap = "false";
            string hidescalebar = "false";

            

            uxWidth = new TextBox();
            uxWidth.Width = 150;


            uxHeight = new TextBox();
            uxHeight.Width = 150;


            uxViewStyle = new DropDownList();
            uxViewStyle.Width = 150;
            uxViewStyle.Items.Add(new ListItem("Road", "road"));
            uxViewStyle.Items.Add(new ListItem("Aerial", "aerial"));
            uxViewStyle.Items.Add(new ListItem("Birdseye", "birdseye"));

            uxDashboardMode = new DropDownList();
            uxDashboardMode.Width = 150;
            uxDashboardMode.Items.Add(new ListItem("Normal", "normal"));
            uxDashboardMode.Items.Add(new ListItem("Small", "small"));
            uxDashboardMode.Items.Add(new ListItem("Tiny", "tiny"));
            uxDashboardMode.Items.Add(new ListItem("Hide", "hide"));


            uxShowMiniMap = new CheckBox();
            uxShowMiniMap.Text = "Yes";

            uxHideScalebar = new CheckBox();
            uxHideScalebar.Text = "Yes";


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
                }
                catch (Exception ex)
                {

                }

            }

            //AddControl("Map Width", "Width of map including px, % or em.", uxWidth,true,false);
            //AddControl("Map Width", "Width of map including px, % or em.", uxWidthUnits,false,true);
            //AddControl("Map Height", "Width of map including px, % or em.", uxHeight);
            //AddControl("Map Height", "Width of map including px, % or em.", uxHeightUnits);
            //AddControl("View Style", "Default view style of map.", uxViewStyle);
            //AddControl("Dashboard Mode", "Default display mode of dashboard", uxDashboardMode);
            //AddControl("Show Minimap", "Yes indicates Minimap will be displayed.", uxShowMiniMap);
            //AddControl("Hide Scalebar", "Yes indicates Scalebar will be hidden.", uxHideScalebar);

            AddLiteral("<table width='500px' cellpadding=0 cellspacing=0>");

            AddLiteral("<tr>");
            AddLiteral("<td><label>Map Width</label><br/>");
            AddControl(uxWidth);
            AddLiteral("<br/><small>Width of the map including px, % or em.</small>");
            AddLiteral("<br/><br/></td>");

            AddLiteral("<td><label>Map Height</label><br/>");
            AddControl(uxHeight);
            AddLiteral("<br/><small>Height of the map including px, % or em.</small>");
            AddLiteral("<br/><br/></td>");
            AddLiteral("<tr>");

            AddLiteral("<tr>");
            AddLiteral("<td><label>View Style</label><br/>");
            AddControl(uxViewStyle);
            AddLiteral("<br/><br/></td>");

            AddLiteral("<td><label>Dashboard Mode</label><br/>");
            AddControl(uxDashboardMode);
            AddLiteral("<br/><br/></td>");
            AddLiteral("<tr>");


            AddLiteral("<tr>");
            AddLiteral("<td><label>Show Mini-map</label><br/>");
            AddControl(uxShowMiniMap);
            AddLiteral("<br/><br/></td>");

            AddLiteral("<td><label>Hide Scalebar</label><br/>");
            AddControl(uxHideScalebar);
            AddLiteral("<br/><br/></td>");
            AddLiteral("<tr>");


            AddLiteral("</table>");

        }

        private void AddLiteral(string text)
        {
            base.ContentTemplateContainer.Controls.Add(new LiteralControl(text));
        }
        private void AddControl(Control control)
        {
            base.ContentTemplateContainer.Controls.Add(control);
        }

        private void AddControl(string label, string description, Control control,bool starttag, bool endtag)
        {
            if (starttag)
            {
                base.ContentTemplateContainer.Controls.Add(new LiteralControl("<p>"));
                base.ContentTemplateContainer.Controls.Add(new LiteralControl("<label>" + label + "</label><br/>"));
            }

            

            if (endtag)
            {
                base.ContentTemplateContainer.Controls.Add(new LiteralControl("<br/>"));
                base.ContentTemplateContainer.Controls.Add(new LiteralControl("<small>" + description + "</small>"));
                base.ContentTemplateContainer.Controls.Add(new LiteralControl("</p>"));
            }
        }

    }

    
    public class bingMapOptionsPrevalueEditor : System.Web.UI.WebControls.PlaceHolder, umbraco.interfaces.IDataPrevalue
    {
        #region IDataPrevalue Members

        // referenced datatype
        private umbraco.cms.businesslogic.datatype.BaseDataType _datatype;


        public bingMapOptionsPrevalueEditor(umbraco.cms.businesslogic.datatype.BaseDataType DataType)
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
