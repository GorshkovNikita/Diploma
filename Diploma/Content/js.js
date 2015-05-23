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
                clearMarkers();
                $('#source_point').val(data["Latitude"] + ';' + data["Longitude"]);
            }
            else if (data["ID"] == "2")
                $('#target_point').val(data["Latitude"] + ';' + data["Longitude"]);
            map.addLayer(marker);
            markerLayers.push(marker);
        }
        else {
            clear();
            alert("Невозможно построить маршрут!");
        }
        /*$.getJSON("http://localhost:58377/Home/GetFullPath", function (data) {
            if (data != null) {
                clearPolylines();
                drawPath(data.Points);
                $('#path_info_length').html('Длина маршрута = ' + data.Length + ' м');
                $('#path_info_time').html('Время построения = ' + data.RunTime + ' с');
            }
        });*/
    });
}

function clear() {
    clearMarkers();
    clearPolylines();
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
