<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Diploma.Models.Path>" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Домашняя страница — приложение ASP.NET MVC
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="map">

    </div>
    <% var serializer = new System.Web.Script.Serialization.JavaScriptSerializer(); %>
    <script>
        var array = <%= serializer.Serialize(Model.Points) %>;
        drawPath(array);
    </script>
</asp:Content>