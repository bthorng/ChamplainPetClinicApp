﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ChamplainPetClinicApp.Models {
    public class User {
        public User(string email, string password) {
            this.email = email;
            this.password = password;
        }

        public string email { get; set; }
        public string password { get; set; }
    }
}
