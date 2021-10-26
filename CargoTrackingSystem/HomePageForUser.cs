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
            LoadMap(new PointLatLng(Convert.ToDouble(txtLatitude.Text), Convert.ToDouble(txtLongitude.Text)));
            AddMarker(new PointLatLng(Convert.ToDouble(txtLatitude.Text), Convert.ToDouble(txtLongitude.Text)));
            //map.DragButton = MouseButtons.Left;
            //map.MapProvider = GMapProviders.GoogleMap;
            //double lat = Convert.ToDouble(txtLatitude.Text);
            //double longt = Convert.ToDouble(txtLongitude.Text);
            //map.Position = new PointLatLng(lat, longt);
            //map.MinZoom = 5;
            //map.MaxZoom = 100;
            //map.Zoom = 10;
            //PointLatLng point = new PointLatLng(lat, longt);
            //GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.red_dot);

            //// 1. Create a Overlay
            //GMapOverlay markers = new GMapOverlay("markers");

            //// 2. Add all available markers to that Overlay
            //markers.Markers.Add(marker);

            //// 3. Cover map with Overlay
            //map.Overlays.Add(markers);

        }

        private void HomePageForUser_Load_1(object sender, EventArgs e)
        {
            GMapProviders.GoogleMap.ApiKey = AppConfig.Key;
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            map.CacheLocation = @"cache";
            map.DragButton = MouseButtons.Left;
            map.MapProvider = GMapProviders.GoogleMap;
            map.ShowCenter = false;
            map.SetPositionByKeywords("Izmit,Turkey");
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
        private void LoadMap(PointLatLng point)
        {
            map.Position = point;
        }
        private void AddMarker(PointLatLng pointToAdd, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot)
        {
            var markers = new GMapOverlay("markers");
            var marker = new GMarkerGoogle(pointToAdd,markerType);
            markers.Markers.Add(marker);
            map.Overlays.Add(markers);

        }

        private List<String> GetAddress(PointLatLng point)
        {
            List<Placemark> placemarks = null;
            var statusCode = GMapProviders.GoogleMap.GetPlacemarks(point, out placemarks);

            if (statusCode == GeoCoderStatusCode.OK && placemarks!=null)
            {
                List<String> addresses = new List<string>();
                foreach (var placemark in placemarks)
                {
                    addresses.Add(placemark.Address);
                }
                return addresses;

            }
            return null;
        }

        private List<Placemark> temp()
        {
            List<Placemark> placemarks = new List<Placemark>();
            return placemarks;
        }

        private void map_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var point = map.FromLocalToLatLng(e.X, e.Y);
                double lat = point.Lat;
                double lng = point.Lng;

                txtLatitude.Text = lat + "";
                txtLongitude.Text = lng + "";
                LoadMap(point);
                AddMarker(point);
                var addresses = GetAddress(point);
                if (addresses!=null)
                {
                    txtAddress.Text = "Address: \n-----------------\n" + String.Join(",", addresses.ToArray());
                }
                else
                {
                    txtAddress.Text = "Address couldn't find!";
                }
            }
        }
    }
}
