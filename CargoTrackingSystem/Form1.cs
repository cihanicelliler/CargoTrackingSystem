using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Cloud.Firestore;

namespace CargoTrackingSystem
{
    public partial class Form1 : Form
    {
        FirestoreDb database;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"cargo-tracking-system.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            database = FirestoreDb.Create("cargo-tracking-system");
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txtBoxUserName.Text;
            string password = txtBoxPassword.Text;
            HomePageForUser homePageForUser = new HomePageForUser();
            homePageForUser.ShowDialog();
            this.Hide();
            //GetAllData_By_Username(userName, password);
            //Add_User_With_AutoID();
            //Update_Document();
        }

        void Add_User_With_AutoID()
        {
            CollectionReference coll = database.Collection("users");
            Dictionary<string, object> data1 = new Dictionary<string, object>()
            {
                {"userName","Cihan" },
                {"userPassword","123" }
            };
            coll.AddAsync(data1);
            MessageBox.Show("Data Added Succesfully");

        }

        async void Update_Document()
        {
            DocumentReference docref = database.Collection("users").Document("Ub0oWlSJavFdKoVwUftj");
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {"userName","ahmet" },
                {"userPassword","12345" }
            };
            DocumentSnapshot snap = await docref.GetSnapshotAsync();

            if (snap.Exists)
            {
                await docref.SetAsync(data);
            }
        }

        async void GetAllData_By_Username(string userName, string password)
        {
            Query Qref = database.Collection("users").WhereEqualTo("userName", userName).WhereEqualTo("userPassword", password);
            QuerySnapshot snap = await Qref.GetSnapshotAsync();

            foreach (DocumentSnapshot docsnap in snap)
            {
                User user = docsnap.ConvertTo<User>();
                if (docsnap.Exists)
                {
                    HomePageForUser objUI = new HomePageForUser();
                    objUI.Show();
                }
                else
                {
                    MessageBox.Show("You are not authorized!");
                }
            }

        }

      
    }
}