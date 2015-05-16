<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Diploma.Models.Path>" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Домашняя страница — приложение ASP.NET MVC
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="map">

    </div>
    <% //var serializer = new System.Web.Script.Serialization.JavaScriptSerializer(); %>
    <script>
        //var map = L.map('map').setView(L.latLng(55.6003, 37.6230), 13);
        var map = displayMap();
        map.on('click', getLatLng);
        //var array =  //serializer.Serialize(Model.Points) %>;
        //drawPath(array);
    </script>
</asp:Content>