async function createMapBoxMap(mapOptions) {
    window.map = new mapboxgl.Map({
        container: mapOptions.container, // container ID
        // Choose from Mapbox's core styles, or make your own style with Mapbox Studio
        style: mapOptions.style, // style URL
        center: [mapOptions.center.longitude, mapOptions.center.latitude], // starting position [lng, lat]
        zoom: mapOptions.zoom // starting zoom
    });
}

function flyTo(flyToOptions) {
    
    window.map.flyTo({
        center: [flyToOptions.center.longitude, flyToOptions.center.latitude],
        zoom: flyToOptions.zoom,
        speed: flyToOptions.speed,
        curve: flyToOptions.curve,
    });
} 

// This should be in a Markers and Controls class, but here for now
function addMarker(position, options) {
    const marker = new mapboxgl.Marker({
        draggable: true
    }).setLngLat([position.coords.longitude, position.coords.latitude])
        .addTo(window.map);
}

function addGeolocateControl() {
    window.map.addControl(
        new mapboxgl.GeolocateControl({
            positionOptions: {
                enableHighAccuracy: true
            },
            trackUserLocation: true,
            showUserHeading: true
        })
    );
}

function getPosition(options){
    return new Promise((resolve, reject) =>
        navigator.geolocation.getCurrentPosition(resolve, reject, options)
    );
}