<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="bingMapAddress.ascx.cs" Inherits="Cidean.Umbraco.bingMaps.bingMapAddress" %>


<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.1/jquery.min.js"></script>
<script type="text/javascript" src="http://ecn.dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.2"></script>

<script type="text/javascript" language=javascript>
    
    
    var addressControl,latitudeControl,longitudeControl;
    
      function setLongitudeLatitude()
	  {     
	        var map = new VEMap('myMap');
            map.LoadMap();
	  		map.Find('', $(".bingAddress").val(),VEFindType.Businesses, null, 0, 1, true, true, true, true, setLongitudeLatitude_callback);
	  }
	  
	  function setLongitudeLatitude_callback(layer, resultsArray, places, hasMore, veErrorMessage)
      {
        var placeLatLong = places[0].LatLong + "";  
        var longlat = placeLatLong.split(",");
        $(".bingLongitude").val(longlat[0]);
        $(".bingLatitude").val(longlat[1]);
        $(".bingMessage").show();
      }
</script>

<p>
<asp:TextBox ID="uxAddress" runat="server" TextMode=MultiLine Rows=3 cssclass="bingAddress" Width="400px" />
</p>
<p><input type="button" value="Get Longitude/Latitude" onclick="setLongitudeLatitude();" /></p>
<p>
<label>Latitude:</label><asp:TextBox ID="uxLatitude" runat="server" cssclass="bingLatitude" />
<label>Longitude:</label><asp:TextBox ID="uxLongitude" runat="server" cssclass="bingLongitude" />
</p>
<p style="display:none; color:Green;" class="bingMessage">Longitude and latitude have been updated.</p>
<p style="position:relative;">
<div id="myMap" style="position:absolute;height:400px;width:400px;display:none;" ></div>
</p>