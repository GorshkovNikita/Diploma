<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Домашняя страница — приложение ASP.NET MVC
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="wrapper">
        <form id="route_request_form" method="post">
            Точки маршрута:
            <div id="points_block">
                <input name="points" class="form_elem way_point" type="text" readonly /><br />
                <input name="points" class="form_elem way_point" type="text" readonly /><br />
            </div>
            <div id="add_point" class="form_elem">Добавить точку</div><br />
            Вид маршрута: <br />
            <select name="route_type" class="form_elem">
                <option selected value="short">Кратчайший</option>
                <option value="safe">Безопасный</option>
                <option value="sport">Спортивный</option>
            </select>
            <br />
            Алгоритм построения кратчайшего маршрута:<br />
            <select name="short_algorithm" class="form_elem">
                <option selected value="dijkstra">Алгоритм Дейкстры</option>
                <option value="astar">Алгоритм A*</option>
            </select>
            <br />
            Алгоритм построения субоптимальных маршрутов:<br />
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
            <p id="path_info_safty">

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
                try {
                    var pathInfo = JSON.parse(data);
                    clearPolylines();
                    drawPath(pathInfo["Path"]);
                    $('#path_info_length').html("Длина маршрута = " + pathInfo["Length"] + " км");
                    $('#path_info_time').html("Время построения = " + pathInfo["RunTime"] + " с");
                    if (pathInfo["SafetyFactor"] != "0")
                        $('#path_info_safty').html("Мера безопасности = " + pathInfo["SafetyFactor"]);
                }
                catch (err) {
                    alert(data);
                }
            }
        });
        $('#route_request_form').on('click', clearPolylines);
        $('#add_point').on('click', addPoint);
        //$('.delete_point').on('click', deletePoint);
    </script>
</asp:Content>