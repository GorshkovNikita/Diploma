function drawPath(array) {
    var line = new Array;
    for (var i = 0; i < array.length; i++)
    {
        line.push(L.latLng(array[i].Latitude, array[i].Longitude));
    }
    var map = L.map('map').setView(L.latLng(array[0].Latitude, array[0].Longitude), 13);
    L.tileLayer('http://{s}.tiles.mapbox.com/v3/examples.map-i875mjb7/{z}/{x}/{y}.png', {
        attribution: 'Map data &copy; <a href="http://openstreetmap.org">OpenStreetMap</a> contributors, <a href="http://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="http://mapbox.com">Mapbox</a>',
        maxZoom: 18
    }).addTo(map);
    var path = L.polyline(line, { color: 'red' }).addTo(map);
}