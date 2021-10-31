using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrackingSystem
{
    class UsersInfo
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string Fullname { get; set; }
        public string NICno { get; set; }

        private static string error = "Username does not exist!";

        public static void ShowError()
        {
            System.Windows.Forms.MessageBox.Show(error);
        }

        public static bool IsEqual(UsersInfo user1, UsersInfo user2)
        {
            if (user1 == null || user2 == null) { return false; }

            if (user1.Username != user2.Username)
            {
                error = "Username does not exist!";
                return false;
            }

            else if (user1.Password != user2.Password)
            {
                error = "Username and password does not match!";
                return false;
            }

            return true;
        }

    }
}
