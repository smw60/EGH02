var raster = new ol.layer.Tile({
    source: new ol.source.OSM(),
    name: 'OSM'
});
var vector = new ol.layer.Vector({
    source: new ol.source.Vector({
    }),
    style: new ol.style.Style({
        fill: new ol.style.Fill({
            color: 'rgba(255, 255, 255, 0.2)'
        }),
        stroke: new ol.style.Stroke({
            color: '#ffcc33',
            width: 2
        }),
        image: new ol.style.Circle({
            radius: 7,
            fill: new ol.style.Fill({
                color: '#ffcc33'
            })
        })
    })
});

var layers1 = new ol.layer.Tile({
    source: new ol.source.TileWMS({
        url: 'http://195.50.2.82:8080/geoserver/EGH/wms?',
        params: {
            'LAYERS': 'EGH:RegionMap',
            'VERSION': '1.1.1',
            'FORMAT': 'image/png',
            'TILED': true
        },
        serverType: 'geoserver'
    })
});
var layers2 = new ol.layer.Tile({
    source: new ol.source.TileWMS({
        url: 'http://195.50.2.82:8080/geoserver/EGH/wms?',
        params: {
            'LAYERS': 'EGH:EcoLocalPolyMap',
            'VERSION': '1.1.1',
            'FORMAT': 'image/png',
            'TILED': true
        },
        visible: false,
        serverType: 'geoserver'
    })
});

var layers3 = new ol.layer.Tile({
    source: new ol.source.TileWMS({
        url: 'http://195.50.2.82:8080/geoserver/EGH/wms?',
        params: {
            'LAYERS': 'EGH:EcoRepublicPolyMap',
            'VERSION': '1.1.1',
            'FORMAT': 'image/png',
            'TILED': true
        },
        serverType: 'geoserver'
    })
});

var layers = [raster, layers1, layers2, layers3, vector];
var myFormat = function (dgts) {
    return (
        function (coord1) {
            var coord2 = [coord1[1], coord1[0]];
            return ol.coordinate.toStringHDMS(coord2, dgts);
        });
}
var mousePositionControl = new ol.control.MousePosition({
    coordinateFormat: myFormat(0),
    projection: 'EPSG:4326',
    className: 'custom-mouse-position',
    target: document.getElementById('mouse-position'),
    undefinedHTML: '&nbsp;'
});
var scaleLineControl = new ol.control.ScaleLine();
var view = new ol.View({
    center: [3200000, 7100000],
    zoom: 6.8
})
var map = new ol.Map({
    controls: ol.control.defaults({
        attributionOptions:
        ({
            collapsible: false
        }),
    }).extend([mousePositionControl, scaleLineControl]),
    layers: layers,
    target: 'map',
    view: view

});

function hideLayer(layerName) {
    layerName.setVisible(false);
}

function toggleLayer(layerName) {
    if (layerName.getVisible() == true) {
        layerName.setVisible(false);
    } else {
        layerName.setVisible(true);
    }
}

map.on('click', function (evt) {
    var coordinate = evt.coordinate;
    var template = '{x}';
    var hdms = ol.coordinate.format(ol.proj.transform(coordinate, 'EPSG:3857', 'EPSG:4326'), template, 4);
    var res = "";
    var template = '{y}';
    document.getElementById('Latitude').value = deg_to_dms(hdms);

    document.getElementById('Lat_m').value = deg_to_dmsm(hdms);
    document.getElementById('Lat_s').value = deg_to_dmss(hdms);
    var hdms2 = ol.coordinate.format(ol.proj.transform(coordinate, 'EPSG:3857', 'EPSG:4326'), template, 4);
    document.getElementById('Lngitude').value = deg_to_dms(hdms2);
    document.getElementById('Lng_m').value = deg_to_dmsm(hdms2);
    document.getElementById('Lng_s').value = deg_to_dmss(hdms2);
});





function deg_to_dms(hdms) {
    var d = Math.floor(hdms);
    var minfloat = (hdms - d) * 60;
    var m = Math.floor(minfloat);
    var secfloat = (minfloat - m) * 60;
    var s = Math.round(secfloat);
    if (s == 60) {
        m++;
        s = 0;
    }
    if (m == 60) {
        d++;
        m = 0;
    }
    return (d);
}
function deg_to_dmsm(hdms) {
    var d = Math.floor(hdms);
    var minfloat = (hdms - d) * 60;
    var m = Math.floor(minfloat);
    var secfloat = (minfloat - m) * 60;
    var s = Math.round(secfloat);
    if (s == 60) {
        m++;
        s = 0;
    }
    if (m == 60) {
        d++;
        m = 0;
    }
    return (m);
}
function deg_to_dmss(hdms) {
    var d = Math.floor(hdms);
    var minfloat = (hdms - d) * 60;
    var m = Math.floor(minfloat);
    var secfloat = (minfloat - m) * 60;
    var s = Math.round(secfloat);
    if (s == 60) {
        m++;
        s = 0;
    }
    if (m == 60) {
        d++;
        m = 0;
    }
    return (s);
}
function centerMap() {
    var lonlat = document.getElementById('coords').value;
    var lonlat1 = lonlat.split(",");
    var lonlat2 = lonlat.split(",").map(Number);

    map.getView().setCenter(ol.proj.transform(lonlat2, 'EPSG:4326', 'EPSG:3857'));
    map.getView().setZoom(7.0);
}
var Modify = {
    init: function () {
        this.select = new ol.interaction.Select();
        map.addInteraction(this.select);

        this.modify = new ol.interaction.Modify({
            features: this.select.getFeatures()
        });
        map.addInteraction(this.modify);

        this.setEvents();
    },
    setEvents: function () {
        var selectedFeatures = this.select.getFeatures();

        this.select.on('change:active', function () {
            selectedFeatures.forEach(selectedFeatures.remove, selectedFeatures);
        });
    },
    setActive: function (active) {
        this.select.setActive(active);
        this.modify.setActive(active);
    }
};
Modify.init();

var optionsForm = document.getElementById('options-form');

var Draw = {
    init: function () {
        map.addInteraction(this.Point);
        this.Point.setActive(false);


    },
    Point: new ol.interaction.Draw({
        source: vector.getSource(),
        type: ('Point'),

    }),

    getActive: function () {
        return this.activeType ? this[this.activeType].getActive() : false;
    },
    setActive: function (active) {
        var type = optionsForm.elements['geom-type'].value;
        if (active) {
            this.activeType && this[this.activeType].setActive(false);
            this[type].setActive(true);
            this.activeType = type;
        } else {
            this.activeType && this[this.activeType].setActive(false);
            this.activeType = null;
        }
    }
};
Draw.init();

optionsForm.onchange = function (e) {
    var type = e.target.getAttribute('name');
    var value = e.target.value;
    if (type == 'geom-type') {
        Draw.getActive() && Draw.setActive(true);
    } else if (type == 'interaction') {
        if (value == 'modify') {
            Draw.setActive(false);
            Modify.setActive(true);
        } else if (value == 'draw') {
            Draw.setActive(true);
            Modify.setActive(false);
        }
    }
};

Draw.setActive(true);
Modify.setActive(false);

var snap = new ol.interaction.Snap({
    source: vector.getSource()
});
map.addInteraction(snap);


var clearButton = document.getElementById('clear');
clearButton.addEventListener('click', function (event) {
    vector.getSource().clear();

});