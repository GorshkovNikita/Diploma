﻿function drawPath(array) {
    var line = new Array;
    for (var i = 0; i < array.length; i++)
    {
        line.push(L.latLng(array[i].Latitude, array[i].Longitude));
    }
    var map = L.map('map').setView(L.latLng(array[0].Latitude, array[0].Longitude), 13);
    var path = L.polyline(line, { color: 'red' }).addTo(map);
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
        if (data != null)
            L.marker(L.latLng(parseFloat(data["Latitude"]), parseFloat(data["Longitude"]))).addTo(map);
        else
            alert("Точка не найдена!");
        $.getJSON("http://localhost:58377/Home/GetFullPath", function (data) {
            if (data != null) {
                var line = new Array;
                for (var i = 0; i < data.length; i++) {
                    line.push(L.latLng(data[i].Latitude, data[i].Longitude));
                }
                L.polyline(line, { color: 'red' }).addTo(map);
            }
        });
    });
    
}

