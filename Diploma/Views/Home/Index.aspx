<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Diploma.Models.Path>" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Домашняя страница — приложение ASP.NET MVC
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="map">

    </div>
    <% //var serializer = new System.Web.Script.Serialization.JavaScriptSerializer(); %>
    <script>
        var map = L.map('map').setView(L.latLng(55.6003, 37.6230), 13);
        map.on('click', getLatLng);
        L.tileLayer('http://{s}.tiles.mapbox.com/v3/examples.map-i875mjb7/{z}/{x}/{y}.png', {
            attribution: 'Map data &copy; <a href="http://openstreetmap.org">OpenStreetMap</a> contributors, <a href="http://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="http://mapbox.com">Mapbox</a>',
            maxZoom: 18
        }).addTo(map);
        //alert(coords.lat);
        //displayMap();
        //var array =  //serializer.Serialize(Model.Points) %>;
        //drawPath(array);
    </script>
</asp:Content>