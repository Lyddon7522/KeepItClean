export async function createMapBoxMap() {
    const map = new mapboxgl.Map({
        container: 'map', // container ID
        // Choose from Mapbox's core styles, or make your own style with Mapbox Studio
        style: 'mapbox://styles/mapbox/satellite-streets-v12', // style URL
        center: [-93.6250, 41.5868], // starting position [lng, lat]
        zoom: 10 // starting zoom
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
    
    const options = {
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
    }
}

export function getPosition(options){
    return new Promise((resolve, reject) =>
        navigator.geolocation.getCurrentPosition(resolve, reject, options)
    );
}