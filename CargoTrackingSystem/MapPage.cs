using FireSharp.Response;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CargoTrackingSystem
{
    public partial class MapPage : Form
    {
        public MapPage()
        {
            InitializeComponent();

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.Controls.Clear();
            this.InitializeComponent();
            routeFunction();
        }

        private void MapPage_Load(object sender, EventArgs e)
        {
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            gMap.CacheLocation = @"cache";
            gMap.SetPositionByKeywords("Türkiye");
            gMap.DragButton = MouseButtons.Left;
            gMap.MapProvider = GMapProviders.GoogleMap;
            gMap.ShowCenter = false;
            gMap.MinZoom = 5;
            gMap.MaxZoom = 100;
            gMap.Zoom = 7;
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = (8 * 1000);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        void routeFunction()
        {
            GMapProviders.GoogleMap.ApiKey = AppConfig.Key;
            gMap.DragButton = MouseButtons.Left;
            gMap.MapProvider = GMapProviders.GoogleMap;
            gMap.MaxZoom = 100;
            gMap.MinZoom = 1;
            gMap.Zoom = 10;
            for (int i = 0; i < GlobalArray.globalPoints.Count; i++)
            {
                double lat = Convert.ToDouble(GlobalArray.globalPoints[Convert.ToInt32(GlobalArray.totalArray[GlobalArray.index, i])].Lat);
                double lng = Convert.ToDouble(GlobalArray.globalPoints[Convert.ToInt32(GlobalArray.totalArray[GlobalArray.index, i])].Lng);
                gMap.Position = new PointLatLng(lat, lng);
                PointLatLng point = new PointLatLng(lat, lng);
                GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.red_dot);
                //to create a overly
                GMapOverlay markers = new GMapOverlay("markers");
                //to add all markers to the overly
                markers.Markers.Add(marker);
                //to show on google maps
                gMap.Overlays.Add(markers);
                if (i + 1 < GlobalArray.globalPoints.Count)
                {
                    Console.WriteLine(Convert.ToInt32(GlobalArray.totalArray[GlobalArray.index, i]).ToString() + Convert.ToInt32(GlobalArray.totalArray[GlobalArray.index, i + 1]).ToString());
                    var route = GoogleMapProvider.Instance.GetRoute(GlobalArray.globalPoints[Convert.ToInt32(GlobalArray.totalArray[GlobalArray.index, i])], GlobalArray.globalPoints[Convert.ToInt32(GlobalArray.totalArray[GlobalArray.index, i + 1])], false, false, 14);
                    var r = new GMapRoute(route.Points, "My Route")
                    {
                        Stroke = new Pen(Color.Red, 5)
                    };
                    var routes = new GMapOverlay("routes");
                    routes.Routes.Add(r);
                    gMap.Overlays.Add(routes);

                }

            }
        }
    }
}
