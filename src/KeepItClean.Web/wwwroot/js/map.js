async function createMapBoxMap() {
    window.map = new mapboxgl.Map({
        container: "map",
        style: "mapbox://styles/mapbox/satellite-streets-v12",
        center: [-93.6250, 41.5868],
        zoom: 10
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