<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="bingMapOptions.ascx.cs" Inherits="Cidean.Umbraco.bingMaps.bingMapOptions" %>

<p>
<label>Height</label>
<asp:TextBox ID="uxHeight" runat="server"></asp:TextBox>
</p>
<p>
<label>Width</label>
<asp:TextBox ID="uxWidth" runat="server"></asp:TextBox>
</p>
<label>View Style</label>
<asp:DropDownList ID="uxViewStyle" runat="server">
<asp:ListItem Text="Road" Value="road" />
<asp:ListItem Text="Aerial" Value="aerial" />
<asp:ListItem Text="Birdseye" Value="birdseye" />
</asp:DropDownList>
<p>
<label>Dashboard Mode</label>
<asp:DropDownList ID="uxDashboardMode" runat="server">
<asp:ListItem Text="Normal" Value="normal" />
<asp:ListItem Text="Small" Value="small" />
<asp:ListItem Text="Tiny" Value="tiny" />
<asp:ListItem Text="Hide" Value="hide" />
</asp:DropDownList>
</p>
<p>
<label>Show Mini Map</label>
<asp:CheckBox ID="uxShowMiniMap" runat="server" Text="Yes"></asp:CheckBox>
</p>
<p>
<label>Hide Scalebar</label>
<asp:CheckBox ID="uxHideScalebar" runat="server" Text="Yes"></asp:CheckBox>
</p>