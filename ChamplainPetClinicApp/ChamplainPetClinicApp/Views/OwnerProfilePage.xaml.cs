using ChamplainPetClinicApp.Models;
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
    [QueryProperty(nameof(OwnerId), nameof(OwnerId))]
    public partial class OwnerProfilePage : ContentPage {

        public string OwnerId { get; set; }

        public OwnerProfilePage() {
            InitializeComponent();
        }

        protected override async void OnAppearing() {
            base.OnAppearing();
            BindingContext = new OwnerProfilePageViewModel(OwnerId);
        }
    }
}