// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

///Reset counter
function reset_counter() {
    localStorage.removeItem('on_load_counter');
    var p = localStorage.getItem('on_load_counter');
    console.log("P " + p);
    //localStorage.removeItem('on_load_counter', m);
}
//document.getElementById('reset').addEventListener('click', reset_counter);  
///

////Use JQuery Ajax to fill out the state and city from zipcode
function is_int(value) {
    if ((parseFloat(value) == parseInt(value)) && !isNaN(value)) {
        return true;
    } else {
        return false;
    }
}
$(".fancy-form div").hide();
$("#zip").keyup(function () {
    // Cache
    var el = $(this);
    // Did they type five integers?
    if ((el.val().length == 5) && (is_int(el.val()))) {
        // Call Ziptastic for information
        $.ajax({
            url: "https://zip.getziptastic.com/v2/US/" + el.val(),
            cache: false,
            dataType: "json",
            type: "GET",
            success: function (result, success) {
                $(".zip-error").slideUp(200);
                $("#city").val(result.city);
                $("#state").val(result.state);
                $("#county").val(result.county);
                $(".fancy-form").slideDown();
                $("#zip").blur();
                $("#zip").focus();
                //$("#address1").show().focus();
            },
            error: function (result, success) {
                $(".zip-error").slideDown(300);
                $("#zip").focus();
            }
        });

    } else if (el.val().length < 5) {
        $(".zip-error").slideUp(200);
        $("#city").val('');
        $("#state").val('');
        $("#county").val('');
    };

});
////Use JQuery Ajax to fill out the state and city from zipcode --- Ends

////Using Google Maps API
//Using code from this website: https://developers.google.com/maps/documentation/javascript/geolocation
function initMap() {
    var map, infoWindow, i, marker;

    map = new google.maps.Map(document.getElementById('map'), {
        //center: { -34.397, 150.644 },
        center: { lat: -34.397, lng: 150.644 },
        zoom: 6
    });
    infoWindow = new google.maps.InfoWindow({});

    ///
    //urlGet = "@Url.Content("~/User/GetMembersLoc")";
    // $(this).data('request-url');
    var urlGet = $('#map').data('map-url');
    var sendLocPlaces = $('#map').data('send-loc');
    var redirectPage = $('#map').data('redir-page');
    var userId = $('#map').data('user-id');    

    // Try HTML5 geolocation.
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            try {
                pos = {
                    lat: position.coords.latitude,
                    lng: position.coords.longitude
                };
            } catch (err) {

            }
            MapPlaces(map, sendLocPlaces, redirectPage, userId);            

            fetch(urlGet)
                .then(function (response) {
                    return response.text();
                })
                .then((data) => {
                    var stringify = JSON.parse(data);
                    /*For this for, I use information of these two websites: https://medium.com/"at"limichelle21/integrating-google-maps-api-for-multiple-locations-a4329517977a and
                    https://www.taniarascia.com/google-maps-apis-for-multiple-locations/ */
                    marker = new google.maps.Marker({ position: pos, map: map, title: "My Pos" });

                    for (i = 0; i < stringify.length; i++) {
                        marker = new google.maps.Marker({
                            position: new google.maps.LatLng(stringify[i]["latitude"], stringify[i]["longitude"]),
                            map: map,
                            title: stringify[i]["fullName"]
                        });
                        google.maps.event.addListener(marker, 'click', (function (marker, i) {
                                return function () {
                                    infowindow.setContent(stringify[i]["fullName"]);
                                    infowindow.open(map, marker);
                                }
                            })(marker, i));
                    }                    
                    map.setCenter(pos);       
                })

            /*For this for, I use information of these two websites: https://medium.com/"at"limichelle21/integrating-google-maps-api-for-multiple-locations-a4329517977a and 
            https://www.taniarascia.com/google-maps-apis-for-multiple-locations/ */

            //for (var i = 0; i < pos.length; i++) {
            //    marker = new google.maps.Marker({
            //        position: new google.maps.LatLng(pos[i][1], pos[i][2]),
            //        map: map,
            //        title: pos[i][0]
            //    });
            //    google.maps.event.addListener(
            //        marker,
            //        'click',
            //        (function (marker, i) {
            //            return function () {                                
            //                infowindow.setContent(pos[i][0]);
            //                infowindow.open(map, marker);
            //            }
            //        })(marker, i));
            //}
            //map.setCenter(posMine);
            //console.log(posMine);
        }, function () {
            handleLocationError(true, infoWindow, map.getCenter());
        });
    } else {
        // Browser doesn't support Geolocation
        handleLocationError(false, infoWindow, map.getCenter());
    }
}  
function handleLocationError(browserHasGeolocation, infoWindow, pos) {
    infoWindow.setPosition(pos);
    infoWindow.setContent(browserHasGeolocation ?
        'Error: The Geolocation service failed.' :
        'Error: Your browser doesn\'t support geolocation.');
    infoWindow.open(map);
}

function MapPlaces(maps, sendLocPlaces, redirectPage, userId) {
    var map, infoWindow;
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            try {
                pos = {
                    lat: position.coords.latitude,
                    lng: position.coords.longitude
                };                
            } catch (err) {

            }

            // Create the places service.
            var service = new google.maps.places.PlacesService(maps);
            var typeArr = ['hospital', 'fire_station', 'police']

            for (i = 0; i < typeArr.length; i++) {
                service.nearbySearch({
                    location: pos,
                    radius: 5000,
                    type: [typeArr[i]]
                },
                function (results, status, pagination) {
                    if (status !== 'OK') return;

                    createMarkers(results, sendLocPlaces, redirectPage, userId);
                    getNextPage = pagination.hasNextPage && function () {
                        pagination.nextPage();
                    };
                });

                /*For this for, I use information of these two websites: https://medium.com/"at"limichelle21/integrating-google-maps-api-for-multiple-locations-a4329517977a and
                https://www.taniarascia.com/google-maps-apis-for-multiple-locations/ */
            }

            // Perform a nearby search.
            
            
        }, function () {
            handleLocationError(true, infoWindow, map.getCenter());
        });
    }

}
function createMarkers(places, sendLocPlaces, redirectPage, userId) {
    var bounds = new google.maps.LatLngBounds();
    //var marker;

    var s = parseInt(localStorage.getItem('on_load_counter'));
    console.log("S " + s);

    if (s === null || s === NaN || ((s >= 3 && s <= 5))) {               //s === NaN || 
        s = 0;
        if (s === 0) {
            for (var i = 0, place; place = places[i]; i++) {
                var image = {
                    url: place.icon,
                    size: new google.maps.Size(71, 71),
                    origin: new google.maps.Point(0, 0),
                    anchor: new google.maps.Point(17, 34),
                    scaledSize: new google.maps.Size(25, 25)
                };

                var marker = new google.maps.Marker({
                    map: map,
                    icon: image,
                    title: place.name,
                    position: place.geometry.location
                });

                bounds.extend(place.geometry.location);

                console.log(place.name + ", " + place.vicinity);// + ", " +
                //place.geometry.location.lat + ", " + place.geometry.location.lng);
                console.log(place);
                //places type URL: https://developers.google.com/places/supported_types
                //place search URL: https://developers.google.com/places/web-service/search
                //place search query URL: https://developers.google.com/maps/documentation/javascript/places#find_place_from_query
                //place searches URL: https://developers-dot-devsite-v2-prod.appspot.com/maps/documentation/javascript/examples/place-search

                console.log(place.types[0]);
                console.log(place.geometry.location.lat());
                console.log(place.geometry.location.lng());

                var urlToSendPlaces = sendLocPlaces;
        
               //Fetch
                fetch(urlToSendPlaces, {
                    method: "POST",
                    body: JSON.stringify({
                        //UserId: parseInt(document.getElementById("userId").value),
                        //latitude: document.getElementById("lat").value,
                        //longitude: document.getElementById("lon").value
                        //UserId: parseInt(@userId),
                        UserId: parseInt(userId),
                        PlaceLat: place.geometry.location.lat(),
                        PlaceLon: place.geometry.location.lng(),
                        PlaceName: place.name,
                        PlaceVicinity: place.vicinity,
                        PlaceType: place.types[0]
                    }),
                    headers: {
                        'Accept': "application/json",
                        "Content-Type": "application/json"
                    }
                }).then(function (response) {
                    if (response.ok) {
                        return response.text()
                    }
                    else {
                        console.log(response.text());
                        // alert("Error")
                    }
                }).then(function (Data) {
                    if (Data != "1") {
                        alert(Data)
                    } else {
                        document.location.href = redirectPage
                    }
                })                
            }
            maps.fitBounds(bounds);
        }
    }
    else {
        s++;
    }
    localStorage.setItem("on_load_counter", s);    
}
////
////Adding Locaion to Database when click on button
function geoFindMe(userId) {

    //Using code from this website: https://developer.mozilla.org/en-US/docs/Web/API/Geolocation_API
    function success(position) {
        const lat = position.coords.latitude;
        const lon = position.coords.longitude;

        //var urlToSendLcoc = "@Url.Content("~/User/Add")";
        var urlToSendLcoc = $('#sendLoc').data('send-loc');
        var urlBackIndex = $('#sendLoc').data('back-index');
        var n = localStorage.getItem('on_load_counter');

        var todaynew = new Date();
        var datetime = todaynew.getFullYear() + "-" + (todaynew.getMonth() + 1) + "-" + todaynew.getDate() + " " + todaynew.getHours() +
            ":" + todaynew.getMinutes() + ":" + todaynew.getSeconds() + "." + todaynew.getMilliseconds();

        console.log(n);

        fetch(urlToSendLcoc, {
            method: "POST",
            body: JSON.stringify({
                //UserId: parseInt(document.getElementById("userId").value),
                //latitude: document.getElementById("lat").value,
                //longitude: document.getElementById("lon").value
                //UserId: parseInt(@userId),
                UserId: parseInt(userId),
                latitude: lat,
                longitude: lon,
                dateAdded: datetime
            }),
            headers: {
                'Accept': "application/json",
                "Content-Type": "application/json"
            }
        }).then(function (response) {
            if (response.ok) {
                return response.text()
            }
            else {
                console.log(response.text());
                // alert("Error")
            }
        }).then(function (Data) {
            if (Data != "1") {
                alert(Data)
            } else {
                document.location.href = urlBackIndex;
            }
        })
    }
    function error() {
        //status.textContent = 'Unable to retrieve your location';
    }
    if (!navigator.geolocation) {
        //status.textContent = 'Geolocation is not supported by your browser';
    } else {
        //status.textContent = 'Locating…';
        navigator.geolocation.getCurrentPosition(success, error);
    }
}
////
///Sending Geolocation of the Current User to the Add Action
function GetLoc(userId, sendLoc, redirectPage) {
    //GeoLocation
    var mapLinkUrl = 'https://www.openstreetmap.org/#map=18/'; //${latitude}/${longitude}

    function success(position) {
        const lat = position.coords.latitude;
        const lon = position.coords.longitude;

        //var urlToSendLcoc = "@Url.Content("~/User/Add")";
        var urlToSendLcoc = sendLoc;
        var n = localStorage.getItem('on_load_counter');
        var todaynew = new Date();
        var datetime = todaynew.getFullYear() + "-" + (todaynew.getMonth() + 1) + "-" + todaynew.getDate() + " " + todaynew.getHours() +
            ":" + todaynew.getMinutes() + ":" + todaynew.getSeconds() + "." + todaynew.getMilliseconds();
        console.log("N "+ n + " and " + datetime);
        if (n === "NaN") {
            n = null;
        }
        //Counter
        if (n === null || n === NaN) {
            n = 0;

            if (n === 0) {
                //Fetch
                fetch(urlToSendLcoc, {
                    method: "POST",
                    body: JSON.stringify({
                        //UserId: parseInt(document.getElementById("userId").value),
                        //latitude: document.getElementById("lat").value,
                        //longitude: document.getElementById("lon").value
                        //UserId: parseInt(@userId),
                        UserId: parseInt(userId),
                        latitude: lat,
                        longitude: lon,
                        dateAdded: datetime
                    }),
                    headers: {
                        'Accept': "application/json",
                        "Content-Type": "application/json"
                    }
                }).then(function (response) {
                    if (response.ok) {
                        return response.text()
                    }
                    else {
                        console.log(response.text());
                        // alert("Error")
                    }
                }).then(function (Data) {
                    if (Data != "1") {
                        alert(Data)
                    } else {
                        document.location.href = redirectPage
                    }
                })
            }
        }
        else {
            n++;
        }

        localStorage.setItem("on_load_counter", n);

        //document.getElementById('counter').innerHTML = n;
    }
    function error() {
        //status.textContent = 'Unable to retrieve your location';
    }
    if (!navigator.geolocation) {
        //status.textContent = 'Geolocation is not supported by your browser';
    } else {
        //status.textContent = 'Locating…';
        navigator.geolocation.getCurrentPosition(success, error);
    }
}
///
/// Send Geolocation of the Current to the Member table on the Usercontroller Edit Action
function GetLastLocMember(userName, toSendLoc, toRedirect) {
    function success(position) {
        const lat = position.coords.latitude;
        const lon = position.coords.longitude;
        var m = parseInt(localStorage.getItem('on_load_counter'));
        console.log("M " + m);
        var urlToSendLoc = toSendLoc;

        //Counter
        if (m === 0 || m === NaN || m === "NaN") {
            m = 0;

            if (m === 0) {
                //Fetch
                fetch(urlToSendLoc, {
                    method: "POST",
                    body: JSON.stringify({
                        fullName: userName,
                        latitude: lat,
                        longitude: lon
                    }),
                    headers: {
                        'Accept': "application/json",
                        "Content-Type": "application/json"
                    }
                }).then(function (response) {
                    if (response.ok) {
                        return response.text()
                    }
                    else {
                        console.log(response.text());
                        // alert("Error")
                    }
                }).then(function (Data) {
                    if (Data != "1") {
                        alert(Data)
                    } else {
                        document.location.href = toRedirect;
                    }
                })
            }
        }
        else {
            m++;
        }
        localStorage.setItem("on_load_counter", m);
        //document.getElementById('counter').innerHTML = n;
    }
    function error() {
        //status.textContent = 'Unable to retrieve your location';
    }
    if (!navigator.geolocation) {
        //status.textContent = 'Geolocation is not supported by your browser';
    } else {
        //status.textContent = 'Locating…';
        navigator.geolocation.getCurrentPosition(success, error);
    }
}
//DataTable script from 
function tablePerPage() {    
    $('#perPage').DataTable({
        //"responsive": true,
        //"processing": true,
        //"orderMulti": false,
        //"filter": true,
        
        //"columnDefs": [
        //    {
        //        "targets": [2],
        //        "visible": false,
        //        "searchable": false
        //    },
        //    {
        //        "targets": [3],
        //        "visible": false
        //    }
        //], 
        //"columns": [
        //    { "width": "20%" },
        //    { "width": "20%" },
        //    null
        //    //null,
        //    //null
        //],
        "pageLength": 10
    });    
}
