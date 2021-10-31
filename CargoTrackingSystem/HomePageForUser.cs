using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;
using Newtonsoft.Json;
using System.IO;
using System.Net;


namespace CargoTrackingSystem
{
    public partial class HomePageForUser : Form
    {
        private string[,] totalArray = new string[100, 100];
        private double[,] totalKmArray = new double[100, 100];
        private string[,] globalArray = new string[100, 100];
        int deleteForMarkers = 0;

        IFirebaseConfig ifc = new FirebaseConfig()
        {
            AuthSecret = "cW0MJa6318nZ1zWKSGlOsnwuzN3yYGDnIRdHhOSz",
            BasePath = "https://cargo-tracking-system-default-rtdb.firebaseio.com/"
        };
        IFirebaseClient client;
        public HomePageForUser()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            try
            {
                client = new FireSharp.FirebaseClient(ifc);
            }

            catch
            {
                MessageBox.Show("No Internet or Connection Problem");
            }
        }
        private void HomePageForUser_Load(object sender, EventArgs e)
        {
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            map.CacheLocation = @"cache";
            map.SetPositionByKeywords("Türkiye, izmit");
            map.DragButton = MouseButtons.Left;
            map.MapProvider = GMapProviders.GoogleMap;
            map.ShowCenter = false;
            map.MinZoom = 5;
            map.MaxZoom = 100;
            map.Zoom = 11.5;
        }

        void PostFirebase(string lat, string lng)
        {

            string address = "https://cargo-tracking-system-default-rtdb.firebaseio.com/.json";
            WebRequest request = HttpWebRequest.Create(address);
            WebResponse webRes;
            webRes = request.GetResponse();

            StreamReader streamReader = new StreamReader(webRes.GetResponseStream());
            string getInfos = streamReader.ReadToEnd();

            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(getInfos);

            Dictionary<string, object> coordinates = new Dictionary<string, object>()
            {
                {"lat",lat},
                {"lng",lng }
            };

            Index.index = myDeserializedClass.İndex;
            SetResponse set = client.Set("Coordinates/" + Index.index, coordinates);
            Index.index++;
            SetResponse setİndex = client.Set("İndex", Index.index);
            MessageBox.Show("Successful");
        }
        public class Coordinate
        {
            public string lat { get; set; }
            public string lng { get; set; }
        }


        public class Root
        {
            public List<Coordinate> Coordinates { get; set; }
            public int İndex { get; set; }
        }

        void GetCoordinates()
        {
            string address = "https://cargo-tracking-system-default-rtdb.firebaseio.com/.json";
            WebRequest request = HttpWebRequest.Create(address);
            WebResponse webRes;
            webRes = request.GetResponse();

            StreamReader streamReader = new StreamReader(webRes.GetResponseStream());
            string getInfos = streamReader.ReadToEnd();

            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(getInfos);


            if (myDeserializedClass.Coordinates.Count != 0 || myDeserializedClass.Coordinates.Count != null)
            {
                foreach (var element in myDeserializedClass.Coordinates)
                {
                    string lat = (element != null) ? element.lat : "0";
                    string lng = (element != null) ? element.lng : "0";

                    CoordinatesList.list.Add(lat);
                    CoordinatesList.list.Add(lng);

                }

                for (int i = 0; i < CoordinatesList.list.Count; i = i + 2)
                {
                    if (CoordinatesList.list[i] != null)
                    {
                        GlobalArray.globalPoints.Add(new PointLatLng(Convert.ToDouble(CoordinatesList.list[i]), Convert.ToDouble(CoordinatesList.list[i + 1])));
                    }
                }
                for (int i = 0; i < GlobalArray.globalPoints.Count; i++)
                {
                    listBox1.Items.Add(GlobalArray.globalPoints[i]);

                }
                //kısayol algoritması
                GlobalArray.numberOfElement = GlobalArray.globalPoints.Count - 1;
                int[] arr = new int[GlobalArray.numberOfElement];
                for (int i = 0; i < GlobalArray.numberOfElement; i++)
                {
                    arr[i] = i + 1;
                }

                Dijikstra(arr, 200);
                //elemanları belirleme
                for (int i = 0; i < GlobalArray.numberOfElement; i++)
                {
                    int[] tempArray = new int[arr.Length];
                    for (int j = 0; j < GlobalArray.numberOfElement; j++)
                    {
                        tempArray[j] = Convert.ToInt32(globalArray[i, j]);

                    }
                    Dijikstra(tempArray, i);
                }
                //en kısa yolu bulma
                for (int i = GlobalArray.numberOfElement; i < (GlobalArray.numberOfElement * GlobalArray.numberOfElement); i++)
                {
                    int[] tempArray = new int[GlobalArray.numberOfElement];
                    for (int j = 0; j < GlobalArray.numberOfElement; j++)
                    {
                        tempArray[j] = Convert.ToInt32(globalArray[i, j]);

                    }
                    Dijikstra(tempArray, i);
                }
                //hepsiin başına sıfır ekleme
                for (int i = 0; i < GlobalArray.count; i++)
                {
                    int[] tempArray = new int[GlobalArray.numberOfElement];
                    for (int j = 0; j < GlobalArray.numberOfElement; j++)
                    {
                        tempArray[j] = Convert.ToInt32(globalArray[i, j]);
                    }
                    tempArray = InsertFunction(tempArray, 0);
                    for (int a = 0; a < tempArray.Length; a++)
                    {
                        GlobalArray.totalArray[i, a] = tempArray[a].ToString();
                    }
                }
                resulst();
            }
        }
        void DeplacementFunction(int[] array, int[] valueArray)
        {
            for (int j = 0; j < array.Length; j++)
            {
                int temp = array[array.Length - 1];
                for (int i = array.Length - 1; i > 0; i--)
                {
                    array[i] = array[i - 1];

                }
                array[0] = temp;
                for (int i = valueArray.Length - 1; i >= 0; i--)
                {
                    array = InsertFunction(array, valueArray[i]);
                }
                for (int i = 0; i < array.Length; i++)
                {
                    globalArray[GlobalArray.count, i] = array[i].ToString();
                }
                GlobalArray.count += 1;
                for (int i = 0; i < valueArray.Length; i++)
                {
                    array = RemoveElement(array);
                }
            }
        }

        void DeplacementFunctionForZero(int[] array)
        {
            for (int j = 0; j < array.Length; j++)
            {
                int temp = array[array.Length - 1];
                for (int i = array.Length - 1; i > 0; i--)
                {
                    array[i] = array[i - 1];

                }
                array[0] = temp;
                for (int i = 0; i < array.Length; i++)
                {
                    globalArray[j, i] = array[i].ToString();

                }
                GlobalArray.count += 1;
            }
        }

        int[] InsertFunction(int[] array, int value)
        {
            int[] tempArray = new int[array.Length + 1];
            int pos = 1;

            for (int i = 0; i < array.Length + 1; i++)
            {
                if (i < pos - 1)
                    tempArray[i] = array[i];
                else if (i == pos - 1)
                    tempArray[i] = value;
                else
                    tempArray[i] = array[i - 1];
            }

            return tempArray;
        }

        int[] RemoveElement(int[] tempArray)
        {
            tempArray = tempArray.Where((source, index) => index != 0).ToArray();
            return tempArray;
        }

        void Dijikstra(int[] array, int index)
        {
            if (index != 200)
            {
                if (index < array.Length)
                {
                    int[] valueArray = new int[1];
                    for (int i = 0; i < valueArray.Length; i++)
                    {
                        valueArray[i] = array[i];
                    }
                    for (int i = 0; i < valueArray.Length; i++)
                    {
                        array = RemoveElement(array);
                    }
                    DeplacementFunction(array, valueArray);
                }
                else if (array.Length <= index && index < (array.Length * array.Length))
                {
                    int[] valueArray = new int[2];
                    for (int i = 0; i < valueArray.Length; i++)
                    {
                        valueArray[i] = array[i];
                    }
                    for (int i = 0; i < valueArray.Length; i++)
                    {
                        array = RemoveElement(array);
                    }
                    DeplacementFunction(array, valueArray);
                }

            }
            else if (index == 200)
            {
                DeplacementFunctionForZero(array);
            }


        }

        void resulst()
        {
            totalArray = GlobalArray.totalArray;
            for (int i = 0; i < GlobalArray.count; i++)
            {
                double km = 0;
                for (int j = 0; j < GlobalArray.numberOfElement + 1; j++)
                {
                    km = km + calculateDistance(GlobalArray.globalPoints[Convert.ToInt32(totalArray[i, j])], GlobalArray.globalPoints[Convert.ToInt32(totalArray[i, j + 1])]);
                }
                totalKmArray[i, 0] = km;
                totalKmArray[i, 1] = i;
            }
            double shortest = totalKmArray[0, 0];
            for (int i = 1; i < GlobalArray.count; i++)
            {
                if (totalKmArray[i, 0] < shortest)
                {
                    shortest = totalKmArray[i, 0];
                    GlobalArray.index = Convert.ToInt32(totalKmArray[i, 1]);
                }
            }
            Console.WriteLine("En kısa yol haritası;\n");
            for (int i = 0; i < GlobalArray.numberOfElement + 1; i++)
            {
                Console.WriteLine(totalArray[GlobalArray.index, i]);
            }
            Console.WriteLine("Toplam yol =>" + " " + shortest + "km");

        }

        int calculateDistance(PointLatLng point1, PointLatLng point2)
        {

            var route = GoogleMapProvider.Instance.GetRoute(point1, point2, false, false, 14);
            var r = new GMapRoute(route.Points, "My Route");
            var routes = new GMapOverlay("routes");
            routes.Routes.Add(r);
            map.Overlays.Add(routes);

            return Convert.ToInt32(route.Distance);
        }

        private void btnLoadMap_Click(object sender, EventArgs e)
        {
            GMapProviders.GoogleMap.ApiKey = AppConfig.Key;
            map.DragButton = MouseButtons.Left;
            map.MapProvider = GMapProviders.GoogleMap;
            double lat = Convert.ToDouble(txtLatitude.Text);
            double lng = Convert.ToDouble(txtLongitude.Text);
            map.Position = new PointLatLng(lat, lng);


            PointLatLng point = new PointLatLng(lat, lng);
            GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.red_dot);
            //to create a overly
            GMapOverlay markers = new GMapOverlay("markers");
            //to add all markers to the overly
            markers.Markers.Add(marker);
            //to show on google maps
            map.Overlays.Add(markers);

            map.MaxZoom = 100;
            map.MinZoom = 1;
            map.Zoom = 10;
        }



        private void btnAddPoint_Click(object sender, EventArgs e)
        {
            PostFirebase(txtLatitude.Text, txtLongitude.Text);

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            CoordinatesList.list.Clear();
            GlobalArray.count = 0;
            GlobalArray.globalPoints.Clear();
            GlobalArray.index = 0;
            GlobalArray.numberOfElement = 0;
            Array.Clear(GlobalArray.totalArray, 0, GlobalArray.totalArray.Length);
            Array.Clear(totalArray, 0, totalArray.Length);
            Array.Clear(totalKmArray, 0, totalKmArray.Length);
            Array.Clear(globalArray, 0, globalArray.Length);
            listBox1.Items.Clear();
            GetCoordinates();
        }

        private void btnOpenMap_Click(object sender, EventArgs e)
        {
            MapPage mapPage = new MapPage();
            mapPage.Show();
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


                point = new PointLatLng(lat, lng);
                GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.red_dot);
                //to create a overly
                GMapOverlay markers = new GMapOverlay("markers");
                //to add all markers to the overly
                markers.Markers.Add(marker);
                //to show on google maps
                map.Overlays.Add(markers);




                var addresses = GetAddress(point);
                if (addresses != null)
                    txtAddress.Text = "Address:  \n--------------\n" + String.Join(",", addresses.ToArray());
                else
                    txtAddress.Text = "Adres bulunmadı";
            }
        }
        private List<String> GetAddress(PointLatLng point)
        {
            List<Placemark> placemarks = null;
            var statusCode = GMapProviders.GoogleMap.GetPlacemarks(point, out placemarks);

            if (statusCode == GeoCoderStatusCode.OK && placemarks != null)
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

        private void LoadMap(PointLatLng point)
        {
            map.Position = point;
        }

        private async void btnDeletePoint_Click(object sender, EventArgs e)
        {
            int indexCount = GlobalArray.globalPoints.Count();
            indexCount -= 1;
            FirebaseResponse res = await client.DeleteAsync("Coordinates/" + totalArray[GlobalArray.index, deleteForMarkers]);
            SetResponse setİndex = client.Set("İndex", indexCount);
            deleteForMarkers++;
            MessageBox.Show("Successful!");
          

        }
    }
}
