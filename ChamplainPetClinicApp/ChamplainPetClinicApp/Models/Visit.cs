using System;
using System.Collections.Generic;
using System.Text;

namespace ChamplainPetClinicApp.Models {
    public class Visit {
        public Visit(string visitId, string date, string description, int petId, int practitionerId, bool status) {
            this.visitId = visitId;
            this.date = date;
            this.description = description;
            this.petId = petId;
            this.practitionerId = practitionerId;
            this.status = status;
        }

        public string visitId { get; set; }
        public int petId { get; set; }
        public int practitionerId { get; set; }
        public string date { get; set; }
        public string description { get; set; }
        public bool status { get; set; }
    }
}
