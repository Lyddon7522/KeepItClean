async function createMapBoxMap(mapOptions) {
    window.map = new mapboxgl.Map({
        container: mapOptions.container, // container ID
        // Choose from Mapbox's core styles, or make your own style with Mapbox Studio
        style: mapOptions.style, // style URL
        center: [mapOptions.center.longitude, mapOptions.center.latitude], // starting position [lng, lat]
        zoom: mapOptions.zoom // starting zoom
    });

    map.on('load', () => {
        // Add an image to use as a custom marker
        map.loadImage(
            'https://docs.mapbox.com/mapbox-gl-js/assets/custom_marker.png',
            (error, image) => {
                if (error) throw error;
                map.addImage('custom-marker', image);
                // Add a GeoJSON source with 2 points
                map.addSource('points', {
                    'type': 'geojson',
                    'data': {
                        'type': 'FeatureCollection',
                        'features': [
                            {
                                // feature for Mapbox DC
                                'type': 'Feature',
                                'geometry': {
                                    'type': 'Point',
                                    'coordinates': [
                                        -77.03238901390978, 38.913188059745586
                                    ]
                                },
                                'properties': {
                                    'title': 'Mapbox DC'
                                }
                            },
                            {
                                // feature for Mapbox SF
                                'type': 'Feature',
                                'geometry': {
                                    'type': 'Point',
                                    'coordinates': [-122.414, 37.776]
                                },
                                'properties': {
                                    'title': 'Mapbox SF'
                                }
                            }
                        ]
                    }
                });

                // Add a symbol layer
                map.addLayer({
                    'id': 'points',
                    'type': 'symbol',
                    'source': 'points',
                    'layout': {
                        'icon-image': 'custom-marker',
                        // get the title name from the source's "title" property
                        'text-field': ['get', 'title'],
                        'text-font': [
                            'Open Sans Semibold',
                            'Arial Unicode MS Bold'
                        ],
                        'text-offset': [0, 1.25],
                        'text-anchor': 'top'
                    }
                });
            }
        );
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