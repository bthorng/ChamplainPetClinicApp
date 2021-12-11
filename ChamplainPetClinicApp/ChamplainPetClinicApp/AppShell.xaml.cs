
using ChamplainPetClinicApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ChamplainPetClinicApp {
    public partial class AppShell : Xamarin.Forms.Shell {
        public AppShell() {
            InitializeComponent();

            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(OwnerProfilePage), typeof(OwnerProfilePage));
            Routing.RegisterRoute(nameof(ManageVisitsForPetPage), typeof(ManageVisitsForPetPage));
            Routing.RegisterRoute(nameof(VisitDetailsPage), typeof(VisitDetailsPage));
            Routing.RegisterRoute(nameof(AddVisitPage), typeof(AddVisitPage));
            Routing.RegisterRoute(nameof(VeterinarianProfilePage), typeof(VeterinarianProfilePage));
            Routing.RegisterRoute(nameof(GenericAddVisitPage), typeof(GenericAddVisitPage));
        }
    }
}
