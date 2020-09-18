function initialize() {
    const options = {
        zoom: 4,
        mapTypeId: google.maps.MapTypeId.HYBRID,
        center: { lat: 40.597681, lng: -95.050860 }
    };
    let map = new google.maps.Map(document.getElementById
        ("google-map"), options);

    navigator.geolocation.getCurrentPosition(function (position) {
        let pos = {
            lat: position.coords.latitude,
            lng: position.coords.longitude
        };

        let marker = new google.maps.Marker({ position: pos, map: map });
        map.setCenter(pos);
    });
}
