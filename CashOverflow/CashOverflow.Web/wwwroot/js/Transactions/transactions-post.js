
let map;
let markers = [];
let clickMarker;
// Initialize and add the map
function initMap() {

    //The center location of our map.
    let uluru = { lat: 42.695566, lng: 23.322510 };

    let mapOptions = {
        draggableCursor: 'default',
        //draggingCursor: 'pointer',
    };

    map = new google.maps.Map(document.getElementById('map'), {
        zoom: 14,
        center: uluru,
        ...mapOptions
    });

    let service = new google.maps.places.PlacesService(map);
    let infowindow = new google.maps.InfoWindow();

    // Create the search box and link it to the UI element.
    let input = document.getElementById('placeSearchBox');
    let searchBox = new google.maps.places.SearchBox(input);
    //map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);

    // Bias the SearchBox results towards current map's viewport.
    map.addListener('bounds_changed', function () {
        searchBox.setBounds(map.getBounds());
    });




    function clearMarkers() {
        markers.forEach(function (marker) {
            marker.setMap(null);
        });
        markers = [];
    }

    function clearClickMarker() {
        if (clickMarker) {
            clickMarker.setMap(null);
            clickMarker = null;
        }
    }
    // Listen for the event fired when the user selects a prediction and retrieve more details for that place.
    searchBox.addListener('places_changed', function () {
        var places = searchBox.getPlaces();

        if (places.length == 0) {
            return;
        }

        if (places.length == 1) {
            getDetails({ placeId: places[0].place_id, latLng: places[0].geometry.location });
        }

        // Clear out the old markers.
        clearMarkers();

        // For each place, get the icon, name and location.
        let bounds = new google.maps.LatLngBounds();
        places.forEach(function (place) {
            if (!place.geometry) {
                console.log("Returned place contains no geometry");
                return;
            }

            //var icon = {
            //  url: 'http://ivanpetrov.eu/el/pin-red.png',
            //  size: new google.maps.Size(71, 71),
            //  origin: new google.maps.Point(0, 0),
            //  anchor: new google.maps.Point(17, 34),
            //  scaledSize: new google.maps.Size(25, 25)
            //};

            // Create a marker for each place.
            var marker = new google.maps.Marker({
                map: map,
                //icon: icon,
                title: place.name,
                position: place.geometry.location
            });

            marker.addListener('click', function () {
                getDetails({ placeId: place.place_id, latLng: place.geometry.location });
            })

            markers.push(marker);

            if (place.geometry.viewport) {
                // Only geocodes have viewport.
                bounds.union(place.geometry.viewport);
            } else {
                bounds.extend(place.geometry.location);
            }
        });
        map.fitBounds(bounds);
    });


    google.maps.event.addListener(map, 'click', function (event) {
        event.stop();

        if (event.placeId) {
            clearMarkers();
            getDetails({ placeId: event.placeId, latLng: event.latLng });
        }
    });

    function getDetails(request) {
        clearClickMarker();

        if (request.placeId) {
            service.getDetails({
                placeId: request.placeId
            }, function (result, status) {

                if (status === google.maps.places.PlacesServiceStatus.OK) {
                    //var icon = {
                    //  url: 'http://ivanpetrov.eu/el/pin-green.png',
                    //  size: new google.maps.Size(71, 71),
                    //  origin: new google.maps.Point(0, 0),
                    //  anchor: new google.maps.Point(17, 34),
                    //  scaledSize: new google.maps.Size(25, 25)
                    //};

                    let marker = new google.maps.Marker({
                        //icon: icon,
                        map: map,
                        position: result.geometry.location
                    });

                    clickMarker = marker;

                    infowindow.setContent('<div style="font-size: 15px;"><strong>' + result.name + '</strong></div>' + result.formatted_address + '</div>');
                    infowindow.open(map, marker);
                }

                document.getElementById('name').value = result.name;
                document.getElementById('addr').value = result.formatted_address;
                document.getElementById('plid').value = result.place_id;
                document.getElementById('lat').value = request.latLng.lat();
                document.getElementById('lng').value = request.latLng.lng();

                document.getElementById('inputLocationName').innerHTML = result.name;
                document.getElementById('inputLocationAddress').innerHTML = result.formatted_address;

                //document.getElementById('placeSearchBox').value = result.formatted_address;


            });
        }
    }

    // Try HTML5 geolocation.
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            var pos = {
                lat: position.coords.latitude,
                lng: position.coords.longitude
            };
            map.setCenter(pos);
        });
    }
}


// Amount input field formatting

let oldValue;

$(document).ready(function () {

    let initializeCurrencyInput = function (el) {

        el.on('keyup', function () {
            let element = $(this);
            let val = element.val().replace(/\s/g, '');

            if (!/^\d*([.,]\d{0,2})?$/.test(val)) {
                element.val(oldValue);
                val = oldValue;
            }

            if (val.includes(',')) {
                val = val.replace(',', '.');
            }

            if (val[0] == ',') {
                val = "0" + val;
            }

            element.val(val.replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1 '));

            oldValue = val;
        });

    }

    oldValue = $('#inputAmount').val();
    initializeCurrencyInput($('#inputAmount'));

});


let flag = {
    category: false,
    datepicker: false
};

// Change transaction type

function changeType(type) {
    var income = document.getElementById("categorySwitchIncome");
    var expense = document.getElementById("categorySwitchExpense");
    var transfer = document.getElementById("categorySwitchTransfer");
    var selector = document.getElementById("categorySwitchSelector");

    let incomeCategoriesList = $('#incomeCategoriesList');
    let expenseCategoriesList = $('#expenseCategoriesList');
    let transferCategoriesList = $('#transferCategoriesList');

    switch (type) {
        case 'Expense':
            selector.style.left = income.clientWidth + "px";
            selector.style.width = expense.clientWidth + "px";

            incomeCategoriesList.addClass('category-list-hidden');
            expenseCategoriesList.removeClass('category-list-hidden');
            transferCategoriesList.addClass('category-list-hidden');
            $("#Expense").prop("checked", true);
            $("#Income").prop("checked", false);
            $("#Transfer").prop("checked", false);
            break;
        case 'Income':
            selector.style.left = 0;
            selector.style.width = income.clientWidth + "px";

            incomeCategoriesList.removeClass('category-list-hidden');
            expenseCategoriesList.addClass('category-list-hidden');
            transferCategoriesList.addClass('category-list-hidden');
            $("#Income").prop("checked", true);
            $("#Expense").prop("checked", false);
            $("#Transfer").prop("checked", false);

            break;
        case 'Transfer':
        default:
            selector.style.left = income.clientWidth + expense.clientWidth + 1 + "px";
            selector.style.width = transfer.clientWidth + "px";

            incomeCategoriesList.addClass('category-list-hidden');
            expenseCategoriesList.addClass('category-list-hidden');
            transferCategoriesList.removeClass('category-list-hidden');
            $("#Income").prop("checked", false);
            $("#Expense").prop("checked", false);
            $("#Transfer").prop("checked", true);
    }

    selector.innerHTML = type;
    selector.classList.remove('Income', 'Expense', 'Transfer');
    selector.classList.add(type);
}

function setCategoryId(id, name) {
    $('#inputCategoryId').val(id);
    $('#inputCategoryLabel').text(name);
    if (!flag.category) {
        $('#caregoryWrapper').collapse('hide');
        $('#dateWrapper').collapse('show');
        flag.category = true;
    }
}


let options = {
    language: 'en',
    dateFormat: 'yyyy-mm-dd',
    inline: true,
    onSelect: function (formattedDate, date) {
        $('#inputDateLabel').text(moment(date).format("DD MMM YYYY"));
        $('#inputDate').val(formattedDate);
        if (!flag.datepicker) {
            $('#dateWrapper').collapse('hide');
            $('#inputAmount').focus();
            flag.datepicker = true;
        }
    }
};


$(document).ready(function () {
    $('#inputDatepicker').datepicker(options);
});
