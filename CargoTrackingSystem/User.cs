using System;
using System.Collections.Generic;
using System.Text;
using Google.Cloud.Firestore;

namespace CargoTrackingSystem
{
    [FirestoreData]
    public class User
    {
        [FirestoreProperty]
        public string userName { get; set; }
        [FirestoreProperty]
        public string userPassword { get; set; }
    }
}