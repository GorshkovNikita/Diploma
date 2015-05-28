function drawPath(array) {
    var line = new Array;
    for (var i = 0; i < array.length; i++) {
        line.push(L.latLng(array[i].Latitude, array[i].Longitude));
    }
    polyLine = L.polyline(line, { color: 'red' });
    map.addLayer(polyLine);
    polylineLayers.push(polyLine);
}

function displayMap() {
    var map = L.map('map').setView(L.latLng(55.6003, 37.6230), 13);
    L.tileLayer('http://{s}.tiles.mapbox.com/v3/examples.map-i875mjb7/{z}/{x}/{y}.png', {
        attribution: 'Map data &copy; <a href="http://openstreetmap.org">OpenStreetMap</a> contributors, <a href="http://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="http://mapbox.com">Mapbox</a>',
        maxZoom: 18
    }).addTo(map);
    return map;
}

function getLatLng(e) {
    $.getJSON("http://localhost:58377/Home/Nearest?lat=" + e.latlng.lat + "&lon=" + e.latlng.lng, function (data) {
        if (data != null) {
            marker = L.marker(L.latLng(parseFloat(data["Latitude"]), parseFloat(data["Longitude"])));
            if (data["ID"] == "1") {
                clearAll();
            }
            $('.way_point').eq(parseInt(data["ID"])-1).val(data["Latitude"] + ';' + data["Longitude"]);
            map.addLayer(marker);
            markerLayers.push(marker);
        }
        else {
            clearMap();
            alert("Невозможно построить маршрут!");
        }
    });
}

function clearAll() {
    clearMarkers();
    clearPolylines();
    resetForm();
    resetConfig();
}

function clearMap() {
    clearMarkers();
    clearPolylines();
}

function resetForm() {
    $('#route_request_form').trigger('reset');
}

function resetConfig() {
    $('Home/ResetCurrentConfig');
}

function clearMarkers() {
    for (var i = 0; i < markerLayers.length; i++) {
        map.removeLayer(markerLayers[i]);
    }
}

function clearPolylines() {
    for (var i = 0; i < polylineLayers.length; i++) {
        map.removeLayer(polylineLayers[i]);
    }
}

function addPoint() {
    clearMap();
    resetForm();
    $('#points_block').append('<input id="target_point" name="points" class="form_elem way_point" type="text" readonly /><div class="delete_point" onclick="deletePoint()" class="form_elem">Удалить точку</div><br />');
    $.get('Home/IncPointsCount');
}

function deletePoint() {
    clearMap();
    resetForm();
    $('#points_block').children().last().remove();
    $('#points_block').children().last().remove();
    $('#points_block').children().last().remove();
    $.get('Home/DecPointsCount');
}