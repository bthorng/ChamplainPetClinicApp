using System;
using System.Collections.Generic;
using System.Text;

namespace ChamplainPetClinicApp.Models {
    public class Pet {
        public int id { get; set; }
        public string name { get; set; }
        public string birthDate { get; set; }
        public PetType type { get; set; }
    }
}
