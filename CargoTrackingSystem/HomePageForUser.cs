using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace CargoTrackingSystem
{
    public partial class HomePageForUser : Form
    {
        private List<PointLatLng> _points;
        public HomePageForUser()
        {
            InitializeComponent();
            _points = new List<PointLatLng>();
        }

        private void btnLoadMap_Click(object sender, EventArgs e)
        {
            map.DragButton = MouseButtons.Left;
            map.MapProvider = GMapProviders.GoogleMap;
            double lat = Convert.ToDouble(txtLatitude.Text);
            double longt = Convert.ToDouble(txtLongitude.Text);
            map.Position = new PointLatLng(lat, longt);
            map.MinZoom = 5;
            map.MaxZoom = 100;
            map.Zoom = 10;
            PointLatLng point = new PointLatLng(lat, longt);
            GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.red_dot);

            // 1. Create a Overlay
            GMapOverlay markers = new GMapOverlay("markers");

            // 2. Add all available markers to that Overlay
            markers.Markers.Add(marker);

            // 3. Cover map with Overlay
            map.Overlays.Add(markers);

        }

        private void HomePageForUser_Load_1(object sender, EventArgs e)
        {
            GMapProviders.GoogleMap.ApiKey = AppConfig.Key;
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            map.CacheLocation = @"cache";
            map.DragButton = MouseButtons.Left;
            map.MapProvider = GMapProviders.GoogleMap;
            map.ShowCenter = false;
            map.SetPositionByKeywords("Istanbul,Turkey");
            map.MinZoom = 5;
            map.MaxZoom = 100;
            map.Zoom = 10;
        }

        private void btnAddPoint_Click(object sender, EventArgs e)
        {
            _points.Add(new PointLatLng(Convert.ToDouble(txtLatitude.Text), Convert.ToDouble(txtLongitude.Text)));
        }

        private void btnClearList_Click(object sender, EventArgs e)
        {
            _points.Clear();
        }

        private void btnGetRoute_Click(object sender, EventArgs e)
        {
            var route = GoogleMapProvider.Instance.GetRoute(_points[0], _points[1], false, false,10);

            var r = new GMapRoute(route.Points, "My Route")
            {
                Stroke = new Pen(Color.Red, 5)
            };

            var routes = new GMapOverlay("routes");
            routes.Routes.Add(r);
            map.Overlays.Add(routes);

            lblDistance.Text = route.Distance + "KM";
        }
    }
}
