export function createMapBoxMap(position) {
    
    const map = new mapboxgl.Map({
        container: 'map', // container ID
        // Choose from Mapbox's core styles, or make your own style with Mapbox Studio
        style: 'mapbox://styles/mapbox/satellite-streets-v12', // style URL
        center: [position.coords.longitude, position.coords.latitude], // starting position [lng, lat]
        zoom: 15 // starting zoom
    });

    map.addControl(
        new mapboxgl.GeolocateControl({
            positionOptions: {
                enableHighAccuracy: true
            },
            trackUserLocation: true,
            showUserHeading: true
        })
    );
}

export function getGeolocation() {
  
    function success(position) {
        createMapBoxMap(position);
    }

    function error() {
        alert("Unable to retrieve your location");
    }

    if (!navigator.geolocation) {
        alert("Geolocation is not supported by your browser");
    } else {
        navigator.geolocation.getCurrentPosition(success, error);
    }
}
