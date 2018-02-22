function init_maps() {
    var address = $("#address").text();
    console.log(address);
    var location = ""; var lat = ""; var lng = "";

    if (address !== "" && address !== null) {
        //Création de la map
        var Options = {
            zoom: 12,
            center: new google.maps.LatLng(48.8566667, 2.3509871),
            mapTypeControl: true,
            mapTypeControlOptions: { style: google.maps.MapTypeControlStyle.DROPDOWN_MENU },
            scrollwheel: 0, mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        var map = new google.maps.Map(document.getElementById('google-map'), Options);

        // Récupération des coordonées à partir de l'adresse
        var geocoder = new google.maps.Geocoder();
        geocoder.geocode({ 'address': address }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                map.setCenter(results[0].geometry.location);
                var marker = new google.maps.Marker({
                    map: map,
                    position: results[0].geometry.location
                });
            }
        });
    }
}
var markers = new Array();
var i = 0;
var map = null;
function init_maps_recherche() {
    // Create MAP
    var Options = {
        zoom: 6,
        center: new google.maps.LatLng(48.8566667, 2.3509871),
        mapTypeControl: true,
        mapTypeControlOptions: { style: google.maps.MapTypeControlStyle.DROPDOWN_MENU },
        scrollwheel: 0, mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    map = new google.maps.Map(document.getElementById('recherche-google-map'), Options);

    // Add marker on click 
    google.maps.event.addListener(map, "click", function (event) {
        $("input#inputSearchGoogleMap").val("");
        var latitude = event.latLng.lat();
        var longitude = event.latLng.lng();

        map.setCenter(new google.maps.LatLng(latitude, longitude));
        var marker = new google.maps.Marker({
            map: map,
            position: new google.maps.LatLng(latitude, longitude)
        });

        markers.push(marker);
        //Remove previous marker
        if (markers.length > 1) {
            markers[i].setMap(null);
            i++;
        }
        
        // Calcul distance 
        var geocoder = new google.maps.Geocoder();
        $(".result").find('div.col-sm-3').each(function (e) {
            var card = $(this);
            var address = $(this).data('address');
            geocoder.geocode({ 'address': address }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    var lat = results[0].geometry.location.lat();
                    var long = results[0].geometry.location.lng();

                    var distance = getDistanceFromLatLonInKm(latitude, longitude, lat, long);
                    if (distance >= "50") {
                        card.hide();
                    } else {
                        card.show();
                    }
                }
            });
            
        });
    });
}

function addMarker(address) {
    var latitude = ""; var longitude = "";
    var geocoder = new google.maps.Geocoder();
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            latitude = results[0].geometry.location.lat();
            longitude = results[0].geometry.location.lng();
            map.setCenter(new google.maps.LatLng(latitude, longitude));
            var marker = new google.maps.Marker({
                map: map,
                position: new google.maps.LatLng(latitude, longitude)
            });
            markers.push(marker);
            //Remove previous marker
            if (markers.length > 1) {
                markers[i].setMap(null);
                i++;
            }

            // Calcul distance 
            $(".result").find('div.col-sm-3').each(function (e) {
                var card = $(this);
                var address = $(this).data('address');
                geocoder.geocode({ 'address': address }, function (results, status) {
                    if (status == google.maps.GeocoderStatus.OK) {
                        var lat = results[0].geometry.location.lat();
                        var long = results[0].geometry.location.lng();

                        var distance = getDistanceFromLatLonInKm(latitude, longitude, lat, long);
                        console.log(distance);
                        if (distance >= "50") {
                            card.hide();
                        } else {
                            card.show();
                        }
                    }
                });

            })
        }
    });

    
}

function getDistanceFromLatLonInKm(lat1, lon1, lat2, lon2) {
    var R = 6371; // Radius of the earth in km
    var dLat = deg2rad(lat2 - lat1);  // deg2rad below
    var dLon = deg2rad(lon2 - lon1);
    var a =
      Math.sin(dLat / 2) * Math.sin(dLat / 2) +
      Math.cos(deg2rad(lat1)) * Math.cos(deg2rad(lat2)) *
      Math.sin(dLon / 2) * Math.sin(dLon / 2)
    ;
    var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
    var d = R * c; // Distance in km
    return d;
}

function deg2rad(deg) {
    return deg * (Math.PI / 180)
}

$(function () {
    $(document).on("change", "select#typeAnimaux", function (e) {
        var idType = $(this).find("option:selected").val();
        if (idType === "") {
            $(".result").find('div.col-sm-3').show();
        } else {
            $(".result").find('div.col-sm-3').each(function (e) {
                if (parseInt($(this).data('id')) !== parseInt(idType)) {
                    $(this).hide();
                } else {
                    $(this).show();
                }
            });
        }
    });

    $(document).on('click', "#resetMarker", function (e) {
        $.each(markers, function (index, value) {
            markers[index].setMap(null);
        });
        $(".result").find('div.col-sm-3').show();
        $("input#inputSearchGoogleMap").val("");
    });

    $(document).on('click', '#btSeachGoogleMap', function (e) {
        var address = $("input#inputSearchGoogleMap").val();
        addMarker(address);
        e.preventDefault();
    });
});