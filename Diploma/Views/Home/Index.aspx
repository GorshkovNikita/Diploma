<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Домашняя страница — приложение ASP.NET MVC
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="map">

    </div>
    <div class="test_xml">
        <!--<span id="x0"><%: ViewData["x0"] %></span>
        <span id="y0"><%: ViewData["y0"] %></span>
        <span id="x1"><%: ViewData["x1"] %></span>
        <span id="y1"><%: ViewData["y1"] %></span>
        <span id="x2"><%: ViewData["x2"] %></span>
        <span id="y2"><%: ViewData["y2"] %></span>
        <span id="x3"><%: ViewData["x3"] %></span>
        <span id="y3"><%: ViewData["y3"] %></span>
        <span id="x4"><%: ViewData["x4"] %></span>
        <span id="y4"><%: ViewData["y4"] %></span>
        <span id="x5"><%: ViewData["x5"] %></span>
        <span id="y5"><%: ViewData["y5"] %></span>
        <span id="x6"><%: ViewData["x6"] %></span>
        <span id="y6"><%: ViewData["y6"] %></span>
        <span id="x7"><%: ViewData["x7"] %></span>
        <span id="y7"><%: ViewData["y7"] %></span>
        <span id="x8"><%: ViewData["x8"] %></span>
        <span id="y8"><%: ViewData["y8"] %></span>
        <span id="x9"><%: ViewData["x9"] %></span>
        <span id="y9"><%: ViewData["y9"] %></span>
        <span id="x10"><%: ViewData["x10"] %></span>
        <span id="y10"><%: ViewData["y10"] %></span>
        <span id="x11"><%: ViewData["x11"] %></span>
        <span id="y11"><%: ViewData["y11"] %></span>
        <span id="x12"><%: ViewData["x12"] %></span>
        <span id="y12"><%: ViewData["y12"] %></span>
        <span id="x13"><%: ViewData["x13"] %></span>
        <span id="y13"><%: ViewData["y13"] %></span>-->
    </div>
    <script>
        /*var x0 = $('#x0').html();
        var y0 = $('#y0').html();
        var x1 = $('#x1').html();
        var y1 = $('#y1').html();
        var x2 = $('#x2').html();
        var y2 = $('#y2').html();
        var x3 = $('#x3').html();
        var y3 = $('#y3').html();
        var x4 = $('#x4').html();
        var y4 = $('#y4').html();
        var x5 = $('#x5').html();
        var y5 = $('#y5').html();
        var x6 = $('#x6').html();
        var y6 = $('#y6').html();
        var x7 = $('#x7').html();
        var y7 = $('#y7').html();
        var x8 = $('#x8').html();
        var y8 = $('#y8').html();
        var x9 = $('#x9').html();
        var y9 = $('#y9').html();
        var x10 = $('#x10').html();
        var y10 = $('#y10').html();
        var x11 = $('#x11').html();
        var y11 = $('#y11').html();
        var x12 = $('#x12').html();
        var y12 = $('#y12').html();
        var x13 = $('#x13').html();
        var y13 = $('#y13').html();*/
        var map = L.map('map').setView([55.8212548, 37.7619006], 13);
        L.tileLayer('http://{s}.tiles.mapbox.com/v3/examples.map-i875mjb7/{z}/{x}/{y}.png', {
            attribution: 'Map data &copy; <a href="http://openstreetmap.org">OpenStreetMap</a> contributors, <a href="http://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="http://mapbox.com">Mapbox</a>',
            maxZoom: 18
        }).addTo(map);
        L.marker([55.8212548, 37.7619006]).addTo(map);
        /*var arr = [ L.latLng(x0, y0), L.latLng(x1, y1), L.latLng(x2, y2), L.latLng(x3, y3),
                    L.latLng(x4, y4), L.latLng(x5, y5), L.latLng(x6, y6), L.latLng(x7, y7),
                    L.latLng(x8, y8), L.latLng(x9, y9), L.latLng(x10, y10), L.latLng(x11, y11),
                    L.latLng(x12, y12), L.latLng(x13, y13)];
        var line = L.polyline(arr, { color: 'red' }).addTo(map);*/
    </script>
</asp:Content>