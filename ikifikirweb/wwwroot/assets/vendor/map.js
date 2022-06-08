// Tutorial: https://developers.google.com/maps/documentation/javascript/tutorial

function initMap() {

  // Specify features and elements to define styles.
  var styleArray = 

// Begin map styles. (Get more styles here: https://snazzymaps.com/)
[
    {
        "featureType": "administrative.country",
        "elementType": "geometry.fill",
        "stylers": [
            {
                "saturation": "-35"
            }
        ]
    }
]
// End map style


    var myLatlng = new google.maps.LatLng(41.0616094, 28.985301); // Set coordinates.
  var mapOptions = {
    mapTypeControl: true,
    scrollwheel: false,
    styles: styleArray, // Apply the map style array to the map.
    zoom: 19,
    center: myLatlng
  }
  var map = new google.maps.Map(document.getElementById("tt-map"), mapOptions);

  // Marker image
  var iconBase = 'https://uploads.ikifikir.net/site/';
  var marker = new google.maps.Marker({
      position: myLatlng,
      title:"Hello! We Are Here. :)",
      icon: iconBase + 'map-marker.png'
  });

  // To add the marker to the map, call setMap();
  marker.setMap(map);
}