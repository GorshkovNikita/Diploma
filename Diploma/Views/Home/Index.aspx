<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Домашняя страница — приложение ASP.NET MVC
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="path_info">
        <p id="path_info_length">
            
        </p>
        <p id="path_info_time">
            
        </p>
        <button id="clear_button" value="Очистить карту">Очистить</button>
    </div>
    <div id="map">

    </div>
    <% //var serializer = new System.Web.Script.Serialization.JavaScriptSerializer(); %>
    <script>
        //var map = L.map('map').setView(L.latLng(55.6003, 37.6230), 13);
        var map = displayMap();
        var polyLine;
        var marker;
        var markerLayers = new Array;
        var polylineLayers = new Array;
        map.on('click', getLatLng);
        $('#clear_button').on('click', clear);
        //var array =  //serializer.Serialize(Model.Points) %>;
        //drawPath(array);
    </script>
</asp:Content>