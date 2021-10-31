using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;


namespace CargoTrackingSystem
{
    public partial class RegisterForUser : Form
    {
        IFirebaseConfig ifc = new FirebaseConfig()
        {
            AuthSecret = "cW0MJa6318nZ1zWKSGlOsnwuzN3yYGDnIRdHhOSz",
            BasePath = "https://cargo-tracking-system-default-rtdb.firebaseio.com/"
        };
        IFirebaseClient client;
        public RegisterForUser()
        {
            InitializeComponent();
        }

        private void RegisterForUser_Load(object sender, EventArgs e)
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

        private void btnRegister2_Click(object sender, EventArgs e)
        {
           
            if (string.IsNullOrWhiteSpace(txtBoxUserNameRegister.Text) &&
               string.IsNullOrWhiteSpace(txtBoxPasswordRegister.Text) &&

               string.IsNullOrWhiteSpace(txtBoxUserNameRegister.Text) &&
               string.IsNullOrWhiteSpace(txtBoxPasswordRegister.Text))
            {
                MessageBox.Show("Please Fill All The Fields");
                return;
            }
           

            UsersInfo user = new UsersInfo()
            {
                Username = txtBoxUserNameRegister.Text,
                Password = txtBoxPasswordRegister.Text,
            };

            SetResponse set = client.Set("Users/" + txtBoxUserNameRegister.Text, user);

            MessageBox.Show("Successfully registered!");
        }
    }
}
