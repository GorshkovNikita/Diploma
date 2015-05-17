<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Diploma.Models.Path>" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Домашняя страница — приложение ASP.NET MVC
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="map">

    </div>
    <button id="clearMap" value="Очистить карту">Очистить</button>
    <% //var serializer = new System.Web.Script.Serialization.JavaScriptSerializer(); %>
    <script>
        //var map = L.map('map').setView(L.latLng(55.6003, 37.6230), 13);
        var map = displayMap();
        var polyLine;
        var marker;
        var layers = new Array;
        map.on('click', getLatLng);
        $('clearMap').on('click', mapClear);
        //var array =  //serializer.Serialize(Model.Points) %>;
        //drawPath(array);
    </script>
</asp:Content>