<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="install.ascx.cs" Inherits="Cidean.Umbraco.bingMaps.Install" %>


<%@ Register Assembly="controls" Namespace="umbraco.controls" TagPrefix="umbc1" %>
<%@ Register Assembly="controls" Namespace="umbraco.uicontrols" TagPrefix="umbc2" %>

<h1 style="font-size:13pt;color:#363636;">bing&trade; Maps for umbraco</h1>
<umbc2:Pane runat="server">
<div style="padding:20px;">
<h2 style="font-size:12pt;">Register for your API Key:</h2>
<p>
This package allows you to integrate Microsoft bing&trade; Maps into your Umbraco implementations.  In order to use this package you will need to register for an API key with Microsoft at <a target=_blank href="https://www.bingmapsportal.com">https://www.bingmapsportal.com</a>. 
</p>
</div>
</umbc2:Pane>

<umbc2:Pane ID="Pane1" runat="server">
<div style="padding:20px;">
<h2 style="font-size:12pt;">Update your API Key:</h2>
<p>Enter or paste the API Key your received from Microsoft and click 'Save' to update your configuration:</p>

<p>
<asp:TextBox ID="uxAPIKey" runat="server" CssClass="umbEditorTextField"></asp:TextBox>
<asp:Button ID="btnSave" runat="server" Text="Save" style="border:solid 1px #363636;"  />
</p>
<asp:Panel ID="uxMessage" runat="server" Visible=false style="color:Green;">
Your bing Maps API Key has been saved successfully.
</asp:Panel>
</div>
</umbc2:Pane>

<umbc2:Pane ID="Pane2" runat="server">
<div style="padding:20px;">
<p>
For more information on using this package <a href="http://www.cidean.com/bingmaps" target="_blank">click here</a>.
</p>
<p>
Copyright &copy; 2010 Cidean, LLC.  All rights reserved.
</p>
</div>
</umbc2:Pane>



