using ChamplainPetClinicApp.Services.NetworkServices;
using ChamplainPetClinicApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChamplainPetClinicApp.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OwnersPage : ContentPage {
        public OwnersPage() {
            InitializeComponent();
            BindingContext = new OwnersPageViewModel();
        }
    }
}