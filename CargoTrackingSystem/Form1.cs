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
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;

namespace CargoTrackingSystem
{
    public partial class Form1 : Form
    {
        IFirebaseConfig ifc = new FirebaseConfig()
        {
            AuthSecret = "cW0MJa6318nZ1zWKSGlOsnwuzN3yYGDnIRdHhOSz",
            BasePath = "https://cargo-tracking-system-default-rtdb.firebaseio.com/"
        };
        IFirebaseClient client;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(ifc);
            }

            catch
            {
                MessageBox.Show("No Internet or Connection Problem");
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            #region Condition
            if (string.IsNullOrWhiteSpace(txtBoxUserName.Text) &&
               string.IsNullOrWhiteSpace(txtBoxPassword.Text))
            {
                MessageBox.Show("Please Fill All The Fields");
                return;
            }
            #endregion
            FirebaseResponse res = client.Get("Users/" + txtBoxUserName.Text);
            UsersInfo ResUser = res.ResultAs<UsersInfo>();// database result

            UsersInfo CurUser = new UsersInfo() // USER GIVEN INFO
            {
                Username = txtBoxUserName.Text,
                Password = txtBoxPassword.Text
            };

            if (UsersInfo.IsEqual(ResUser, CurUser))
            {
                HomePageForUser real = new HomePageForUser();
                real.ShowDialog();
                UserName.User = txtBoxUserName.Text;
            }

            else
            {
                UsersInfo.ShowError();
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterForUser register = new RegisterForUser();
            register.Show();
            this.Hide();
        }
    }
}