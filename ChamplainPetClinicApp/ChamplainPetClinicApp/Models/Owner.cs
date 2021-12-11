using System;
using System.Collections.Generic;
using System.Text;

namespace ChamplainPetClinicApp.Models {
    public class Owner {

        public Owner(string firstName, string lastName, string address, string city, string telephone, List<Pet> pets) {
            this.firstName = firstName;
            this.lastName = lastName;
            this.address = address;
            this.city = city;
            this.telephone = telephone;
            this.pets = pets;
        }

        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string telephone { get; set; }
        public List<Pet> pets { get; set; }
        public List<Visit> visits { get; set; }
    }
}
