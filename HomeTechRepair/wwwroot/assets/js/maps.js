let marker = null;
let map;
let lat, lng;

function initMap() {
    map = new google.maps.Map(document.getElementById("map"), {
        zoom: 10,
        zoomControl: true,
        mapTypeControl: false,
        scaleControl: false,
        streetViewControl: false,
        rotateControl: false,
        fullscreenControl: false
    });
    map.addListener("click", (mapsMouseEvent) => {
        setMarker(mapsMouseEvent.latLng);
    });

    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(
            (position) => {
                const pos = {
                    lat: position.coords.latitude,
                    lng: position.coords.longitude,
                };
                map.setCenter(pos);
            }
        );
    }
}

function setMarker(location) {
    if (marker != null) {
        marker.setMap(null);
    }
    marker = new google.maps.Marker({
        position: location,
        map: map
    });
    getMarkerLocation();
}

function getMarkerLocation() {
    if (marker != null) {
        lat = marker.getPosition().lat();
        lng = marker.getPosition().lng();
    }
}

function centerMap(location) {
    map = {
        center: location
    }
}
