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
    [QueryProperty(nameof(VetId), nameof(VetId))]
    public partial class VeterinarianProfilePage : ContentPage {

        public int VetId { get; set; }

        public VeterinarianProfilePage() {
            InitializeComponent();
        }

        protected override async void OnAppearing() {
            base.OnAppearing();
            BindingContext = new VeterinarianProfilePageViewModel(VetId);
        }
    }
}