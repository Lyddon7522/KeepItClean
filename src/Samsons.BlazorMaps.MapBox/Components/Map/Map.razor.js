function createMapBoxMap(mapOptions) {
    window.map = new mapboxgl.Map({
        container: mapOptions.container, // container ID
        // Choose from Mapbox's core styles, or make your own style with Mapbox Studio
        style: mapOptions.style, // style URL
        center: [mapOptions.center.longitude, mapOptions.center.latitude], // starting position [lng, lat]
        zoom: mapOptions.zoom // starting zoom
    });
    
    /*const options = {
        maximumAge: 10000,
        timeout: 5000,
    }
    
    try {
        const position = await getPosition(options);
        map.flyTo({
            center: [position.coords.longitude, position.coords.latitude],
            zoom: 12,
            speed: 0.5,
            curve: 1,
        });
        const marker = new mapboxgl.Marker({
            draggable: true
        }).setLngLat([position.coords.longitude, position.coords.latitude])
            .addTo(map);
    } catch (err) {
        console.log(err.message);
    }*/
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