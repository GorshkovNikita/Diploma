<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Домашняя страница — приложение ASP.NET MVC
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="wrapper">
        <form id="route_request_form" method="post">
            Начальная точка:<br />
            <input id="source_point" name="source_str" class="form_elem" type="text" /><br />
            Конечная точка:<br />
            <input id="target_point" name="target_str" class="form_elem" type="text" /><br />
            <select name="route_type" class="form_elem">
                <option selected value="short">Кратчайший</option>
                <option value="safe">Безопасный</option>
                <option value="sport">Спортивный</option>
            </select>
            <br />
            <select name="short_algorithm" class="form_elem">
                <option selected value="dijkstra">Алгоритм Дейкстры</option>
                <option value="astar">Алгоритм A*</option>
            </select>
            <br />
            <select name="sub_short_algorithm" class="form_elem">
                <option selected value="kshort">К кратчайшие</option>
                <option value="eclosest">E близкие</option>
            </select>
            <br />
            Количество субоптимальных (K)/отклонение от оптимального (E):
            <br />
            <input name="ke" class="form_elem" value="0" />
            <br />
            <input type="submit" value="Построить маршрут" class="form_elem" />
        </form>
        <div id="path_info">
            <p id="path_info_length">
            
            </p>
            <p id="path_info_time">
            
            </p>
            <!--<button id="clear_button" value="Очистить карту">Очистить</button>-->
        </div>
    </div>
    <div id="map"></div>
    <script>
        var map = displayMap();
        var polyLine;
        var marker;
        var markerLayers = new Array;
        var polylineLayers = new Array;
        map.on('click', getLatLng);
        $("#route_request_form").ajaxForm({
            url: '/Home/RouteRequest',
            type: 'post',
            success: function (data) {
                try
                {
                    var pathInfo = JSON.parse(data);
                    drawPath(pathInfo["Path"]);
                }
                catch (err)
                {
                    alert(data);
                }                    
            }
        });
        $('#clear_button').on('click', clear);
        //var array =  //serializer.Serialize(Model.Points) %>;
        //drawPath(array);
    </script>
</asp:Content>